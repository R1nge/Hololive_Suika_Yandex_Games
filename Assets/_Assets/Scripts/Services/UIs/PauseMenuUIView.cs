using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class PauseMenuUIView : MonoBehaviour
    {
        [SerializeField] private Button resume, mainMenu;
        [SerializeField] private Slider musicSlider, vfxSlider;
        [Inject] private PauseMenuUIController _pauseMenuUIController;

        private void OnEnable()
        {
            musicSlider.value = _pauseMenuUIController.MusicVolume;
            vfxSlider.value = _pauseMenuUIController.VfxVolume;
        }

        private void Awake()
        {
            mainMenu.onClick.AddListener(MainMenu);
            resume.onClick.AddListener(Resume);
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            vfxSlider.onValueChanged.AddListener(ChangeSoundVolume);
        }

        private void MainMenu() => _pauseMenuUIController.MainMenu();

        private void Resume() => _pauseMenuUIController.Resume();

        private void ChangeMusicVolume(float volume) => _pauseMenuUIController.ChangeMusicVolume(volume);

        private void ChangeSoundVolume(float volume) => _pauseMenuUIController.ChangeSoundVolume(volume);

        private void OnDestroy()
        {
            mainMenu.onClick.RemoveListener(MainMenu);
            resume.onClick.RemoveListener(Resume);
            musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
            vfxSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
        }
    }
}