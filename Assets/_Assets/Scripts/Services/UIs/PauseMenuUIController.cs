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
        private readonly PlayerInput _playerInput;
        private readonly ContinueService _continueService;
        private readonly TimeRushTimer _timeRushTimer;

        private PauseMenuUIController(AudioService audioService, UIStateMachine uiStateMachine,
            LocalizationService localizationService, PlayerInput playerInput, ContinueService continueService,
            TimeRushTimer timeRushTimer)
        {
            _audioService = audioService;
            _uiStateMachine = uiStateMachine;
            _localizationService = localizationService;
            _playerInput = playerInput;
            _continueService = continueService;
            _timeRushTimer = timeRushTimer;
        }

        public LocalizationService.Language CurrentLanguage => _localizationService.CurrentLanguage;

        public void ChangeLanguage(LocalizationService.Language language) =>
            _localizationService.SetLanguage(language).Forget();

        public void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);

        public void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);

        public void Pause() => _timeRushTimer.Pause();

        public async void Resume()
        {
            await _uiStateMachine.SwitchToPreviousState();
            _playerInput.Enable();
            _timeRushTimer.Resume();
        }

        public async void MainMenu()
        {
            _continueService.Save();
            await _uiStateMachine.SwitchStateUI(UIStateType.MainMenu);
        }

        public float MusicVolume => _audioService.MusicVolume;
        public float VfxVolume => _audioService.VfxVolume;
    }
}