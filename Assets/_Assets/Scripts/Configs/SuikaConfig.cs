using System;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class SuikaConfig : ScriptableObject
    {
        [SerializeField] private SuikaData[] suikas;
        public Suika GetPrefab(int index) => suikas[index].Prefab;
        public int GetPoints(int index)
        {
            index = Mathf.Clamp(index - 1, 0, suikas.Length - 1);
            return suikas[index].Points;
        }

        public bool HasPrefab(int index) => suikas[index].Prefab != null;

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
    }
}