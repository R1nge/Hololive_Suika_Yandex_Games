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
        [SerializeField] private AssetReferenceGameObject gameModeSelectionUI;
        [SerializeField] private AssetReferenceGameObject gameUI;
        [SerializeField] private AssetReferenceGameObject gameOverUI;
        public AssetReferenceGameObject LoadingUI => loadingUI;
        public AssetReferenceGameObject MainMenuUI => mainMenuUI;
        public AssetReferenceGameObject SettingsUI => settingsUI;
        public AssetReferenceGameObject GameModeSelectionUI => gameModeSelectionUI;
        public AssetReferenceGameObject GameUI => gameUI;
        public AssetReferenceGameObject GameOverUI => gameOverUI;
    }
}