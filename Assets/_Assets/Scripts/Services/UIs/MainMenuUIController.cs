using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenuUIController
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ContinueService _continueService;
        private readonly ResetService _resetService;
        public bool HasData => _continueService.HasData;

        public MainMenuUIController(UIStateMachine uiStateMachine, GameStateMachine gameStateMachine, ContinueService continueService, ResetService resetService)
        {
            _uiStateMachine = uiStateMachine;
            _gameStateMachine = gameStateMachine;
            _continueService = continueService;
            _resetService = resetService;
        }

        public async void Continue() => await _gameStateMachine.SwitchState(GameStateType.Continue);

        public async void Play() => await _uiStateMachine.SwitchStateUI(UIStateType.GameModeSelection);

        public async void Settings() => await _uiStateMachine.SwitchStateUI(UIStateType.Settings);

        public async void SkinSelection() => await _gameStateMachine.SwitchState(GameStateType.SkinSelection);
    }
}