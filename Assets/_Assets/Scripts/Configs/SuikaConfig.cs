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
            index = Mathf.Clamp(index - 1, 0, suikas.Length - 1);
            return suikas[index].Points;
        }
        
        public AudioClip GetSound(int index)
        {
            index = Mathf.Clamp(index - 1, 0, suikas.Length - 1);
            return suikas[index].Sound;
        }

        public bool HasPrefab(int index) => suikas[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Suika Prefab;
            public int Points;
            public AudioClip Sound;

            public SuikaData(Suika prefab, int points, AudioClip sound)
            {
                Prefab = prefab;
                Points = points;
                Sound = sound;
            }
        } 
    }
}