using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenuUIView : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [Inject] private MainMenuUIController _mainMenuUIController;

        private void Awake()
        {
           continueButton.onClick.AddListener(Continue);
           playButton.onClick.AddListener(Play); 
           settingsButton.onClick.AddListener(Settings);
        }

        private void Continue() => _mainMenuUIController.Continue();

        private void Play() => _mainMenuUIController.Play();

        private void Settings() => _mainMenuUIController.Settings();

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(Play);
            settingsButton.onClick.RemoveListener(Settings);
        }
    }
}