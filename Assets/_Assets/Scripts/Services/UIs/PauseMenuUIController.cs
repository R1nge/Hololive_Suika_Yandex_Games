using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class PauseMenuUIController
    {
        private readonly AudioService _audioService;
        private readonly UIStateMachine _uiStateMachine;

        private PauseMenuUIController(AudioService audioService, UIStateMachine uiStateMachine)
        {
            _audioService = audioService;
            _uiStateMachine = uiStateMachine;
        }

        public void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);

        public void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);

        public async void Resume() => await _uiStateMachine.SwitchToPreviousState();

        public async void MainMenu() => await _uiStateMachine.SwitchState(UIStateType.MainMenu);
        
        public float MusicVolume => _audioService.MusicVolume;
        public float VfxVolume => _audioService.VfxVolume;
    }
}