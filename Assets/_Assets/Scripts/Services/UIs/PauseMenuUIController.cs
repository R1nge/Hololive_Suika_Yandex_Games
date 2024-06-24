using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class PauseMenuUIController
    {
        private readonly AudioService _audioService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly LocalizationService _localizationService;

        private PauseMenuUIController(AudioService audioService, UIStateMachine uiStateMachine, LocalizationService localizationService)
        {
            _audioService = audioService;
            _uiStateMachine = uiStateMachine;
            _localizationService = localizationService;
        }

        public void ChangeLanguage(LocalizationService.Language language) => _localizationService.SetLanguage(language).Forget();

        public void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);

        public void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);

        public async void Resume() => await _uiStateMachine.SwitchToPreviousState();

        public async void MainMenu() => await _uiStateMachine.SwitchStateUI(UIStateType.MainMenu);
        
        public float MusicVolume => _audioService.MusicVolume;
        public float VfxVolume => _audioService.VfxVolume;
    }
}