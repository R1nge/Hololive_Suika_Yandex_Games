using System;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class Continue : IAsyncState
    {
        private readonly GameModeService _gameModeService;
        private readonly GameStateMachine _gameStateMachine;

        public Continue(GameModeService gameModeService, GameStateMachine gameStateMachine)
        {
            _gameModeService = gameModeService;
            _gameStateMachine = gameStateMachine;
        }

        public async UniTask Enter()
        {
            switch (_gameModeService.GetGameMode())
            {
                case GameModeService.GameMode.None:
                    break;
                case GameModeService.GameMode.Endless:
                    await _gameStateMachine.SwitchState(GameStateType.ContinueEndless);
                    break;
                case GameModeService.GameMode.TimeRush:
                    await _gameStateMachine.SwitchState(GameStateType.ContinueTimeRush);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async UniTask Exit()
        {
        }
    }
}