using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameSelectionUIView : MonoBehaviour
    {
        [SerializeField] private Button endless;
        [SerializeField] private Button timeRush;
        [SerializeField] private Button back;
        [SerializeField] private Button settings;
        [Inject] private GameSelectionUIController _gameSelectionUIController;

        private void Awake()
        {
            endless.onClick.AddListener(SelectEndless);
            timeRush.onClick.AddListener(SelectTimeRush);
            back.onClick.AddListener(MainMenu);
            settings.onClick.AddListener(Settings);
        }

        private void Settings() => _gameSelectionUIController.Settings();

        private void MainMenu() => _gameSelectionUIController.MainMenu();

        private void SelectEndless() => _gameSelectionUIController.SelectEndless();

        private void SelectTimeRush() => _gameSelectionUIController.SelectTimeRush();

        private void OnDestroy()
        {
            endless.onClick.RemoveListener(SelectEndless);
            timeRush.onClick.RemoveListener(SelectTimeRush);
            back.onClick.RemoveListener(MainMenu);
            settings.onClick.RemoveListener(Settings);
        }
    }
}