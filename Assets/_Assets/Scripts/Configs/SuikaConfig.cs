using System;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Suika Config", menuName = "Configs/Suika Config")]
    public class SuikaConfig : ScriptableObject
    {
        [SerializeField] private SuikaData[] suikas;
        public Suika GetPrefab(int index) => suikas[index].Prefab;
        public int GetPoints(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            return suikas[index].Points;
        }
        
        public AudioClip GetSound(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            return suikas[index].Sound;
        }
        
        public Sprite GetSprite(int index)
        {
            index = Mathf.Clamp(index, 0, suikas.Length - 1);
            return suikas[index].Sprite;
        }

        public bool HasPrefab(int index) => suikas[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Suika Prefab;
            public int Points;
            public AudioClip Sound;
            public Sprite Sprite;

            public SuikaData(Suika prefab, int points, AudioClip sound, Sprite sprite)
            {
                Prefab = prefab;
                Points = points;
                Sound = sound;
                Sprite = sprite;
            }
        } 
    }
}