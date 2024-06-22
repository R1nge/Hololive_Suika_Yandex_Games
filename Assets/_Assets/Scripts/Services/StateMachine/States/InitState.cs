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
        private readonly PlayerInput _playerInput;

        public InitState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, YandexService yandexService, PlayerInput playerInput)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
            _playerInput = playerInput;
        }

        public async UniTask Enter()
        {
            _playerInput.Init();
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            await _yandexService.Init();
            await _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public async UniTask Exit()
        {
        }
    }
}