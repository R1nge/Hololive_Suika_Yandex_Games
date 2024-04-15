using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class InitState : IAsyncState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly YandexService _yandexService;

        public InitState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, YandexService yandexService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            await _yandexService.Init();
        }

        public async UniTask Exit()
        {
        }
    }
}