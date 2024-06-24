using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenuUIController
    {
        private readonly UIStateMachine _uiStateMachine;

        public MainMenuUIController(UIStateMachine uiStateMachine) => _uiStateMachine = uiStateMachine;
        
        public void Continue() {}

        public async void Play() => await _uiStateMachine.SwitchStateUI(UIStateType.GameModeSelection);

        public async void Settings() => await _uiStateMachine.SwitchStateUI(UIStateType.Settings);
    }
}