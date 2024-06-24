using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class GameSelectionUIController
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;

        private GameSelectionUIController(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
        }

        public async void MainMenu() => await _uiStateMachine.SwitchStateUI(UIStateType.MainMenu);

        public async void SelectEndless() => await _gameStateMachine.SwitchState(GameStateType.Endless);

        public async void SelectTimeRush() => await _gameStateMachine.SwitchState(GameStateType.TimeRush);
    }
}