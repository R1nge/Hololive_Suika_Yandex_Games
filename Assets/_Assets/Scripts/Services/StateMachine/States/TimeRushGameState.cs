using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.Quests;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using YG;

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
        private readonly GameModeService _gameModeService;
        private readonly InGameTimeCounter inGameTimeCounter;

        public TimeRushGameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine,
            ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput,
            TimeRushTimer timeRushTimer, GameModeService gameModeService, InGameTimeCounter inGameTimeCounter)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
            this.inGameTimeCounter = inGameTimeCounter;
        }

        public async UniTask Enter()
        {
            inGameTimeCounter.Enable();
            _playerInput.Disable();
            _gameModeService.SetGameMode(GameModeService.GameMode.TimeRush);
            await _uiStateMachine.SwitchStateUI(UIStateType.TimeRush);
            _containerFactory.Create();
            var player = _playerFactory.Create();
            player.GetComponent<PlayerController>().SpawnSuika();
            _playerInput.Enable();
            _timeRushTimer.Start();
            YandexMetrica.Send("start_time_rush");
        }

        public async UniTask Exit()
        {
            inGameTimeCounter.Disable();
            _timeRushTimer.Reset();
        }
    }
}