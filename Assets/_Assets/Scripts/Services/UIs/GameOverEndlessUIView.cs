using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverEndlessUIView : MonoBehaviour
    {
        [SerializeField] private Button restart, mainMenu;
        [Inject] private GameOverEndlessUIController _gameOverEndlessUIController;

        private void Awake()
        {
            mainMenu.onClick.AddListener(MainMenu);
            restart.onClick.AddListener(Restart);
        }

        private void MainMenu() => _gameOverEndlessUIController.MainMenu();

        private void Restart() => _gameOverEndlessUIController.Restart();
        
        private void OnDestroy()
        {
            mainMenu.onClick.RemoveListener(MainMenu);
            restart.onClick.RemoveListener(Restart);
        }
    }
}