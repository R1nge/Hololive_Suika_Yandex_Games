using _Assets.Scripts.Services.Audio;
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
        private readonly AudioService _audioService;

        public InitState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, YandexService yandexService, PlayerInput playerInput, AudioService audioService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
            _playerInput = playerInput;
            _audioService = audioService;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            _audioService.Init();
            _playerInput.Init();
            await _yandexService.Init();
            await _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public async UniTask Exit()
        {
        }
    }
}