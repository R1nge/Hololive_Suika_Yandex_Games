using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private SuikaConfig suikaConfig;
        [SerializeField] private SongConfig songConfig;
        public UIConfig UIConfig => uiConfig;
        public GameConfig GameConfig => gameConfig;
        public SuikaConfig SuikaConfig => suikaConfig;
        public SongConfig SongConfig => songConfig;
    }
}