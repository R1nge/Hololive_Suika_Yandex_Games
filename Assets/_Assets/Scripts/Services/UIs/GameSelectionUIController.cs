using _Assets.Scripts.Services.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class GameSelectionUIController
    {
        private readonly GameStateMachine _gameStateMachine;

        private GameSelectionUIController(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        public async void SelectEndless() => await _gameStateMachine.SwitchState(GameStateType.Endless);

        public async void SelectTimeRush() => await _gameStateMachine.SwitchState(GameStateType.TimeRush);
    }
}