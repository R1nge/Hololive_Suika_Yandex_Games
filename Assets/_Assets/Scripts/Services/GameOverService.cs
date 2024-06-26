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
        private readonly GameModeService _gameModeService;

        public GameOverService(GameOverTimer gameOverTimer, GameStateMachine gameStateMachine, TimeRushTimer timeRushTimer, GameModeService gameModeService)
        {
            _gameOverTimer = gameOverTimer;
            _gameStateMachine = gameStateMachine;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
        }

        public void Initialize()
        {
            _gameOverTimer.OnTimerEnded += GameOver;
            _timeRushTimer.OnTimerEnded += GameOver;
        }

        private void GameOver(float a) => GameOver();

        private void GameOver()
        {
            switch (_gameModeService.GetGameMode())
            {
                case GameModeService.GameMode.None:
                    break;
                case GameModeService.GameMode.Endless:
                    _gameStateMachine.SwitchState(GameStateType.GameOverEndless).Forget();
                    break;
                case GameModeService.GameMode.TimeRush:
                    _gameStateMachine.SwitchState(GameStateType.GameOverTimeRush).Forget();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            _gameOverTimer.OnTimerEnded -= GameOver;
            _timeRushTimer.OnTimerEnded -= GameOver;
        }
    }
}