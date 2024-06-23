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

        private readonly Dictionary<int, UniTask<AudioClip>> loadingSounds = new();

        public async UniTask<AudioClip> GetSound(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            var clipReference = suikas[index].Sound;

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
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            var spriteReference = suikas[index].Sprite;

            if (loadingSprites.TryGetValue(index, out var spriteTask))
            {
                return await spriteTask;
            }

            if (spriteReference.OperationHandle.IsValid() && spriteReference.OperationHandle.IsDone)
            {
                return spriteReference.OperationHandle.Result as Sprite;
            }

            var loadTask = spriteReference.LoadAssetAsync<Sprite>().ToUniTask();
            loadingSprites.Add(index, loadTask);
            var sprite = await loadTask;
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