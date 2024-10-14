using System;
using System.Collections.Generic;
using _Assets.Scripts.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService
    {
        private readonly ConfigProvider _configProvider;

        //private SuikaSkinData[] _suikaSkinData;
        private SuikaSkinData[] _selected;
        private readonly Dictionary<int, UniTaskCompletionSource<Sprite>> _loadingSprites = new();
        public int SelectedSkinLength => _selected.Length;

        private SkinService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public async UniTask<Sprite> GetSprite(int index)
        {
            bool isUnlocked = !_selected[index].IsLocked;
            index = Mathf.Clamp(index, 0, _selected.Length - 1);
            var spriteReference = isUnlocked
                ? _selected[index].SuikaSkin.SpriteUnlocked
                : _selected[index].SuikaSkin.SpriteLocked;

            UniTaskCompletionSource<Sprite> completionSource;
            bool isNewTask = false;

            // Lock the dictionary while accessing it to ensure thread safety
            lock (_loadingSprites)
            {
                if (!_loadingSprites.TryGetValue(index, out var existingCompletionSource))
                {
                    // If the task does not exist, create a new UniTaskCompletionSource
                    completionSource = new UniTaskCompletionSource<Sprite>();
                    _loadingSprites.Add(index, completionSource);
                    isNewTask = true;
                }
                else
                {
                    // If the task already exists, use the existing UniTaskCompletionSource
                    completionSource = existingCompletionSource;
                }
            }

            if (isNewTask)
            {
                // If this is a new task, perform the loading and complete the UniTaskCompletionSource
                try
                {
                    if (spriteReference.OperationHandle.IsValid() && spriteReference.OperationHandle.IsDone)
                    {
                        completionSource.TrySetResult(spriteReference.OperationHandle.Result as Sprite);
                    }
                    else
                    {
                        var sprite = await spriteReference.LoadAssetAsync<Sprite>().ToUniTask();
                        completionSource.TrySetResult(sprite);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
                finally
                {
                    // Remove the completion source from the dictionary when the task is done
                    lock (_loadingSprites)
                    {
                        _loadingSprites.Remove(index);
                    }
                }
            }

            // Await the completion source's task
            return await completionSource.Task;
        }

        public void Init()
        {
            _selected = new SuikaSkinData[11];
            //_suikaSkinData = new SuikaSkinData[_configProvider.SuikaConfig.SuikaSkins.Count];

            var i = 0;
            foreach (var skin in _configProvider.SuikaConfig.SuikaSkins)
            {
                _selected[i].SuikaSkin = skin;
                _selected[i].IsLocked = false;//Random.Range(0, 2) == 0;
                i++;
            }
        }

        public void Swap(int index, int targetIndex)
        {
            (_selected[index], _selected[targetIndex]) = (_selected[targetIndex], _selected[index]);
        }

        public void Set(int skinIndex, int positionIndex)
        {
            //_selected[positionIndex] = _suikaSkinData[skinIndex];
        }

        public SuikaSkinData Get(int index)
        {
            return _selected[index];
        }

        public void Reset()
        {
            for (int i = 0; i < _selected.Length; i++)
            {
                //_selected[i] = _suikaSkinData[i];
            }
        }
    }

    public struct SuikaSkinData
    {
        public SuikaConfig.SuikaSkin SuikaSkin;
        public bool IsLocked;
    }
}