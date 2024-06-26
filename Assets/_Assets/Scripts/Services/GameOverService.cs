using System;
using _Assets.Scripts.Services.StateMachine;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class GameOverService : IInitializable, IDisposable
    {
        private readonly GameOverTimer _gameOverTimer;
        private readonly GameStateMachine _gameStateMachine;
        private readonly TimeRushTimer _timeRushTimer;

        public GameOverService(GameOverTimer gameOverTimer, GameStateMachine gameStateMachine, TimeRushTimer timeRushTimer)
        {
            _gameOverTimer = gameOverTimer;
            _gameStateMachine = gameStateMachine;
            _timeRushTimer = timeRushTimer;
        }

        public void Initialize()
        {
            _gameOverTimer.OnTimerEnded += GameOver;
            _timeRushTimer.OnTimerEnded += GameOver;
        }

        private void GameOver(float a) => GameOver();

        private void GameOver() => _gameStateMachine.SwitchState(GameStateType.GameOverEndless).Forget();

        public void Dispose()
        {
            _gameOverTimer.OnTimerEnded -= GameOver;
            _timeRushTimer.OnTimerEnded -= GameOver;
        }
    }
}