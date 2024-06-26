using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using YG;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverTimeRushGameState : IAsyncState
    {
        private readonly YandexService _yandexService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly ScoreService _scoreService;
        private readonly ResetService _resetService;
        
        public GameOverTimeRushGameState(YandexService yandexService, UIStateMachine uiStateMachine,
            ScoreService scoreService, ResetService resetService)
        {
            _yandexService = yandexService;
            _uiStateMachine = uiStateMachine;
            _scoreService = scoreService;
            _resetService = resetService;
        }

        public async UniTask Enter()
        {
            if (_scoreService.Score > YandexGame.savesData.highScoreEndless)
            {
                YandexGame.savesData.highScoreEndless = _scoreService.Score;
                _yandexService.UpdateLeaderBoardScore("timeRush", _scoreService.Score);
                YandexGame.SaveProgress();
            }

            _resetService.Reset();
            await _uiStateMachine.SwitchStateUI(UIStateType.GameOverTimeRush); 
        }

        public async UniTask Exit()
        {
        }
    }
}