using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ContinueEndless : IAsyncState
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerInput _playerInput;
        private readonly ContinueService _continueService;


        public ContinueEndless(UIStateMachine uiStateMachine, ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput, ContinueService continueService)
        {
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _continueService = continueService;
        }
        public async UniTask Enter()
        {
            _playerInput.Disable();
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            var player = _playerFactory.Create();
            player.GetComponent<PlayerController>().SpawnContinue();
            _containerFactory.Create();
            _continueService.Continue();
            await _uiStateMachine.SwitchState(UIStateType.Endless);
            _continueService.UpdateScore();
            _playerInput.Enable();
        }

        public async UniTask Exit()
        {
        }
    }
}