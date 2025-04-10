﻿using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class SettingsUIController
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly LocalizationService _localizationService;
        private readonly AudioService _audioService;

        private SettingsUIController(UIStateMachine uiStateMachine,LocalizationService localizationService, AudioService audioService)
        {
            _uiStateMachine = uiStateMachine;
            _localizationService = localizationService;
            _audioService = audioService;
        }
        
        public LocalizationService.Language CurrentLanguage => _localizationService.CurrentLanguage;
        
        public void ChangeLanguage(LocalizationService.Language language) => _localizationService.SetLanguage(language).Forget();
        
        public void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);
        
        public void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);
        
        public async UniTask Back() => await _uiStateMachine.SwitchToPreviousState();
        
        public float MusicVolume => _audioService.MusicVolume;
        public float VfxVolume => _audioService.VfxVolume;
    }
}