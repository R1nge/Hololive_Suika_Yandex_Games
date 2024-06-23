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
        [SerializeField] private SuikaData[] suikas;
        private readonly Dictionary<int, UniTask<Sprite>> loadingSprites = new();
        public Suika GetPrefab(int index) => suikas[index].Prefab;

        public int GetPoints(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            return suikas[index].Points;
        }

        public async UniTask<AudioClip> GetSound(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            var clipReference = suikas[index].Sound;


            if (clipReference.OperationHandle.IsValid() && clipReference.OperationHandle.IsDone)
            {
                return clipReference.OperationHandle.Result as AudioClip;
            }

            var clip = await clipReference.LoadAssetAsync<AudioClip>().Task;
            return clip;
        }

        public async UniTask<Sprite> GetSprite(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);

            if (loadingSprites.TryGetValue(index, out var spriteTask))
            {
                return await spriteTask;
            }

            var loadTask = LoadSpriteAsync(index);
            loadingSprites.Add(index, loadTask);
            return await loadTask;
        }

        private async UniTask<Sprite> LoadSpriteAsync(int index)
        {
            var spriteReference = suikas[index].Sprite;
            var sprite = await spriteReference.LoadAssetAsync<Sprite>().Task;
            loadingSprites.Remove(index);
            return sprite;
        }

        public bool HasPrefab(int index) => suikas[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Suika Prefab;
            public int Points;
            public AssetReferenceAudioClip Sound;
            public AssetReferenceSprite Sprite;

            public SuikaData(Suika prefab, int points, AssetReferenceAudioClip sound, AssetReferenceSprite sprite)
            {
                Prefab = prefab;
                Points = points;
                Sound = sound;
                Sprite = sprite;
            }
        }
    }
}