using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverTimeRushUIController
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;

        private GameOverTimeRushUIController(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
        }
        
        public void MainMenu() => _uiStateMachine.SwitchStateUI(UIStateType.MainMenu).Forget();

        public void Restart() => _gameStateMachine.SwitchState(GameStateType.TimeRush).Forget();
    }
}