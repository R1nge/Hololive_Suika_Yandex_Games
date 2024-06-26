using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class TimeRushGameState : IAsyncState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerInput _playerInput;
        private readonly TimeRushTimer _timeRushTimer;

        public TimeRushGameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine,
            ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput,
            TimeRushTimer timeRushTimer)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            _playerInput.Disable();
            await _uiStateMachine.SwitchStateUI(UIStateType.Endless);
            _containerFactory.Create();
            var player = _playerFactory.Create();
            player.GetComponent<PlayerController>().SpawnSuika();
            _playerInput.Enable();
            _timeRushTimer.Start();
        }

        public async UniTask Exit()
        {
            _timeRushTimer.Pause();
        }
    }
}