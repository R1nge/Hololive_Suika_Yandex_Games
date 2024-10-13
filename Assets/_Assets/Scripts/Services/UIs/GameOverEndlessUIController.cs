using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverEndlessUIController
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly YandexService _yandexService;

        private GameOverEndlessUIController(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine,
            YandexService yandexService)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
        }

        public void MainMenu()
        {
            _yandexService.ShowVideoAd();
            _uiStateMachine.SwitchStateUI(UIStateType.MainMenu).Forget();
        }

        public void Restart()
        {
            _yandexService.ShowVideoAd();
            _gameStateMachine.SwitchState(GameStateType.Endless).Forget();
        }
    }
}