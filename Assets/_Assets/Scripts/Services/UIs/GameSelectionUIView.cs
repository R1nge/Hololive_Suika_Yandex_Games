using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameSelectionUIView : MonoBehaviour
    {
        [SerializeField] private Button endless;
        [SerializeField] private Button timeRush;
        [Inject] private GameSelectionUIController _gameSelectionUIController;

        private void Awake()
        {
            endless.onClick.AddListener(SelectEndless);
            timeRush.onClick.AddListener(SelectTimeRush);
        }

        private void SelectEndless() => _gameSelectionUIController.SelectEndless();

        private void SelectTimeRush() => _gameSelectionUIController.SelectTimeRush();

        private void OnDestroy()
        {
            endless.onClick.RemoveListener(SelectEndless);
            timeRush.onClick.RemoveListener(SelectTimeRush);
        }
    }
}