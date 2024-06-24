using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverEndlessGameState : IAsyncState
    {
        private readonly YandexService _yandexService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly ScoreService _scoreService;
        private readonly ResetService _resetService;

        public GameOverEndlessGameState(YandexService yandexService, UIStateMachine uiStateMachine, ScoreService scoreService, ResetService resetService)
        {
            _yandexService = yandexService;
            _uiStateMachine = uiStateMachine;
            _scoreService = scoreService;
            _resetService = resetService;
        }

        public async UniTask Enter()
        {
            _yandexService.UpdateLeaderBoardScore(_scoreService.Score);
            _resetService.Reset();
            await _uiStateMachine.SwitchStateUI(UIStateType.GameOverEndless);
        }

        public async UniTask Exit()
        {
        }
    }
}