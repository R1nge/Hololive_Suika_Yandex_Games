using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ContinueTimeRush : IAsyncState
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerInput _playerInput;
        private readonly ContinueService _continueService;
        private readonly TimeRushTimer _timeRushTimer;


        public ContinueTimeRush(UIStateMachine uiStateMachine, ContainerFactory containerFactory,
            PlayerFactory playerFactory, PlayerInput playerInput, ContinueService continueService, TimeRushTimer timeRushTimer)
        {
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _continueService = continueService;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            _playerInput.Disable();
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            var player = _playerFactory.Create();
            player.GetComponent<PlayerController>().SpawnContinue();
            _containerFactory.Create();
            await _continueService.Continue();
            await _uiStateMachine.SwitchState(UIStateType.Endless);
            _continueService.UpdateScore();
            _playerInput.Enable();
            _timeRushTimer.Start();
        }

        public async UniTask Exit()
        {
            _timeRushTimer.Reset();
        }
    }
}