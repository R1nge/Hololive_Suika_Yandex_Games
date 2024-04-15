using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private float[] suikaDropChances;
        public float[] SuikaDropChances => suikaDropChances;
    }
}