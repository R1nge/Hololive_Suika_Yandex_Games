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
        [Inject] private GameSelectionUIController _gameSelectionUIController;

        private void Awake()
        {
            endless.onClick.AddListener(SelectEndless);
            timeRush.onClick.AddListener(SelectTimeRush);
            back.onClick.AddListener(MainMenu);
        }

        private void MainMenu() => _gameSelectionUIController.MainMenu();

        private void SelectEndless() => _gameSelectionUIController.SelectEndless();

        private void SelectTimeRush() => _gameSelectionUIController.SelectTimeRush();

        private void OnDestroy()
        {
            endless.onClick.RemoveListener(SelectEndless);
            timeRush.onClick.RemoveListener(SelectTimeRush);
        }
    }
}