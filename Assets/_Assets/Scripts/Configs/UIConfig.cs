using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject loadingUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject settingsUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject gameOverUI;
        public GameObject LoadingUI => loadingUI;
        public GameObject MainMenuUI => mainMenuUI;
        public GameObject SettingsUI => settingsUI;
        public GameObject GameUI => gameUI;
        public GameObject GameOverUI => gameOverUI;
    }
}