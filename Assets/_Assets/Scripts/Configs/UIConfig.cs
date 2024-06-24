using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject loadingUI;
        [SerializeField] private AssetReferenceGameObject mainMenuUI;
        [SerializeField] private AssetReferenceGameObject settingsUI;
        [SerializeField] private AssetReferenceGameObject pauseUI;
        [SerializeField] private AssetReferenceGameObject gameModeSelectionUI;
        [SerializeField] private AssetReferenceGameObject endlessUI;
        [SerializeField] private AssetReferenceGameObject timeRushUI;
        [SerializeField] private AssetReferenceGameObject gameOverEndlessUI;
        [SerializeField] private AssetReferenceGameObject gameOverTimeRushUI;
        public AssetReferenceGameObject LoadingUI => loadingUI;
        public AssetReferenceGameObject MainMenuUI => mainMenuUI;
        public AssetReferenceGameObject SettingsUI => settingsUI;
        public AssetReferenceGameObject PauseUI => pauseUI;
        public AssetReferenceGameObject GameModeSelectionUI => gameModeSelectionUI;
        public AssetReferenceGameObject EndlessUI => endlessUI;
        public AssetReferenceGameObject TimeRushUI => timeRushUI;
        public AssetReferenceGameObject GameOverEndlessUI => gameOverEndlessUI;
        public AssetReferenceGameObject GameOverTimeRushUI => gameOverTimeRushUI;
    }
}