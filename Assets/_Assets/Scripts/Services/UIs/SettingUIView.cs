using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class SettingUIView : MonoBehaviour
    {
        [SerializeField] private Button back;
        [SerializeField] private Slider musicSlider, vfxSlider;
        [SerializeField] private Button en, ru, tr;
        [SerializeField] private Image selectedEn, selectedRu;
        [Inject] private SettingsUIController _settingsUIController;

        private void Awake()
        {
            back.onClick.AddListener(Back);
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            vfxSlider.onValueChanged.AddListener(ChangeSoundVolume);
            
            en.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.English));
            ru.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.Russian));
            tr.onClick.AddListener(() => ChangeLanguage(LocalizationService.Language.Turkish)); 
        }

        private void Start()
        {
            musicSlider.value = _settingsUIController.MusicVolume;
            vfxSlider.value = _settingsUIController.VfxVolume;
            ChangeLanguage(_settingsUIController.CurrentLanguage);
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
            
            _settingsUIController.ChangeLanguage(language);
        }

        private void ChangeSoundVolume(float volume) => _settingsUIController.ChangeSoundVolume(volume);

        private void ChangeMusicVolume(float volume) => _settingsUIController.ChangeMusicVolume(volume);

        private void Back() => _settingsUIController.Back().Forget();
        
        private void OnDestroy()
        {
            back.onClick.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            vfxSlider.onValueChanged.RemoveAllListeners();
            
            en.onClick.RemoveAllListeners();
            ru.onClick.RemoveAllListeners();
            tr.onClick.RemoveAllListeners();
        }
    }
}