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
        [SerializeField] private Button en, ru, tr;
        [SerializeField] private Image selectedEn, selectedRu;
        [Inject] private PauseMenuUIController _pauseMenuUIController;

        private void Awake()
        {
            mainMenu.onClick.AddListener(MainMenu);
            resume.onClick.AddListener(Resume);
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            vfxSlider.onValueChanged.AddListener(ChangeSoundVolume);
            
            en.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.English));
            ru.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.Russian));
            tr.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.Turkish));
        }

        private void Start()
        {
            musicSlider.value = _pauseMenuUIController.MusicVolume;
            vfxSlider.value = _pauseMenuUIController.VfxVolume;
            ChangeLanguage(_pauseMenuUIController.CurrentLanguage);
        }

        private void ChangeLanguage(LocalizationService.Language language)
        {
            switch (language)
            {
                case LocalizationService.Language.English:
                    selectedEn.gameObject.SetActive(true);
                    selectedRu.gameObject.SetActive(false);
                    break;
                case LocalizationService.Language.Russian:
                    selectedEn.gameObject.SetActive(false);
                    selectedRu.gameObject.SetActive(true);
                    break;
                case LocalizationService.Language.Turkish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
            
            _pauseMenuUIController.ChangeLanguage(language);
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
            
            
            en.onClick.RemoveAllListeners();
            ru.onClick.RemoveAllListeners();
            tr.onClick.RemoveAllListeners();
        }
    }
}