using _Assets.Scripts.Houses;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private SuikaConfig suikaConfig;
        [SerializeField] private SoundsConfig soundsConfig;
        [SerializeField] private HouseItemsConfig houseItemsConfig;
        public UIConfig UIConfig => uiConfig;
        public GameConfig GameConfig => gameConfig;
        public SuikaConfig SuikaConfig => suikaConfig;
        public SoundsConfig SoundsConfig => soundsConfig;
        public HouseItemsConfig HouseItemsConfig => houseItemsConfig;
    }
}