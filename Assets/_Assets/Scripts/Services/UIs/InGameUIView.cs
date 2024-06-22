using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameUIView : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [Inject] private InGameUIController _inGameUIController;

        private void Awake() => pauseButton.onClick.AddListener(Pause);

        private void Pause() => _inGameUIController.Pause();

        private void OnDestroy() => pauseButton.onClick.RemoveListener(Pause);
    }
}