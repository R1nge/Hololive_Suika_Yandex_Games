using _Assets.Scripts.Services.StateMachine;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class TimerRushGameOver : IStartable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly TimeRushTimer _timeRushTimer;

        private TimerRushGameOver(GameStateMachine gameStateMachine, TimeRushTimer timeRushTimer)
        {
            _gameStateMachine = gameStateMachine;
            _timeRushTimer = timeRushTimer;
        }
        
        public void Start()
        {
            _timeRushTimer.OnTimerEnded += GameOver;
        }

        private void GameOver(float time)
        {
            _gameStateMachine.SwitchState(GameStateType.GameOver).Forget();
        }
    }
}