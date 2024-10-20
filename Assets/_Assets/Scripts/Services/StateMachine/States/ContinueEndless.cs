using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.Quests;
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
        private readonly InGameTimeCounter inGameTimeCounter;


        public ContinueEndless(UIStateMachine uiStateMachine, ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput, ContinueService continueService, InGameTimeCounter inGameTimeCounter)
        {
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _continueService = continueService;
            this.inGameTimeCounter = inGameTimeCounter;
        }
        public async UniTask Enter()
        {
            inGameTimeCounter.Enable();
            _playerInput.Disable();
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            await _continueService.Continue();
            var player = _playerFactory.Create();
            await player.GetComponent<PlayerController>().SpawnContinue();
            _containerFactory.Create();
            await _uiStateMachine.SwitchState(UIStateType.Endless);
            _continueService.UpdateScore();
            _playerInput.Enable();
        }

        public async UniTask Exit()
        {
            inGameTimeCounter.Disable();
        }
    }
}