using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Quests;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Wallets;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using YG;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class InitState : IAsyncState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly YandexService _yandexService;
        private readonly PlayerInput _playerInput;
        private readonly AudioService _audioService;
        private readonly LocalizationService _localizationService;
        private readonly ContinueService _continueService;
        private readonly Wallet _wallet;
        private readonly QuestsService questsService;

        public InitState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, YandexService yandexService,
            PlayerInput playerInput, AudioService audioService, LocalizationService localizationService, ContinueService continueService, Wallet wallet, QuestsService questsService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
            _playerInput = playerInput;
            _audioService = audioService;
            _localizationService = localizationService;
            _continueService = continueService;
            _wallet = wallet;
            this.questsService = questsService;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchStateUI(UIStateType.Loading);
            await _yandexService.Init();
            _audioService.Init();
            _playerInput.Init();
            _continueService.Load();
            _wallet.Load();
            questsService.Load();
            await _localizationService.InitYandex(YandexGame.lang);
            await _uiStateMachine.SwitchStateUI(UIStateType.MainMenu);
            YandexMetrica.Send("id");
        }

        public async UniTask Exit()
        {
        }
    }
}