using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenuUIController
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly GameStateMachine _gameStateMachine;

        public MainMenuUIController(UIStateMachine uiStateMachine, GameStateMachine gameStateMachine)
        {
            _uiStateMachine = uiStateMachine;
            _gameStateMachine = gameStateMachine;
        }

        public async void Continue() => await _gameStateMachine.SwitchState(GameStateType.Continue);

        public async void Play() => await _uiStateMachine.SwitchStateUI(UIStateType.GameModeSelection);

        public async void Settings() => await _uiStateMachine.SwitchStateUI(UIStateType.Settings);
    }
}