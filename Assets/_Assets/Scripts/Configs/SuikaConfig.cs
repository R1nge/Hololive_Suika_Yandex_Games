using System;
using System.Collections.Generic;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Suika Config", menuName = "Configs/Suika Config")]
    public class SuikaConfig : ScriptableObject
    {
        [SerializeField] private SuikaData[] suikaData;
        [SerializeField] private SuikaSkin[] suikaSkins;
        public IReadOnlyCollection<SuikaSkin> SuikaSkins => suikaSkins;
        private readonly Dictionary<int, UniTaskCompletionSource<Sprite>> _loadingSprites = new();
        public Suika GetPrefab(int index) => suikaData[index].Prefab;

        public int GetPoints(int index)
        {
            index = Mathf.Clamp(index, 0, suikaData.Length - 1);
            return suikaData[index].Points;
        }

        private readonly Dictionary<int, UniTask<AudioClip>> loadingSounds = new();

        public async UniTask<AudioClip> GetSound(int index)
        {
            index = Mathf.Clamp(index, 0, suikaData.Length - 1);
            var clipReference = suikaSkins[index].Sound;

            if (loadingSounds.TryGetValue(index, out var soundTask))
            {
                return await soundTask;
            }

            if (clipReference.OperationHandle.IsValid() && clipReference.OperationHandle.IsDone)
            {
                return clipReference.OperationHandle.Result as AudioClip;
            }

            var loadTask = clipReference.LoadAssetAsync<AudioClip>().ToUniTask();
            loadingSounds.Add(index, loadTask);
            var clip = await loadTask;
            loadingSounds.Remove(index);
            return clip;
        }

        public async UniTask<Sprite> GetSprite(int index)
        {
            index = Mathf.Clamp(index, 0, suikaData.Length - 1);
            var spriteReference = suikaSkins[index].SpriteUnlocked;

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

        public bool HasPrefab(int index) => suikaData[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Suika Prefab;
            public int Points;

            public SuikaData(Suika prefab, int points)
            {
                Prefab = prefab;
                Points = points;
            }
        }

        [Serializable]
        public struct SuikaSkin
        {
            public AssetReferenceAudioClip Sound;
            public AssetReferenceSprite SpriteUnlocked;
            public AssetReferenceSprite SpriteLocked;
        }
    }
}