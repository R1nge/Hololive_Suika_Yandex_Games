using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverTimeRushUIView : MonoBehaviour
    {
        [SerializeField] private Button restart, mainMenu;
        [Inject] private GameOverTimeRushUIController _gameOverTimeRushUIController;

        private void Awake()
        {
            mainMenu.onClick.AddListener(MainMenu);
            restart.onClick.AddListener(Restart);
        }

        private void MainMenu() => _gameOverTimeRushUIController.MainMenu();

        private void Restart() => _gameOverTimeRushUIController.Restart();
        
        private void OnDestroy()
        {
            mainMenu.onClick.RemoveListener(MainMenu);
            restart.onClick.RemoveListener(Restart);
        }
        
    }
}