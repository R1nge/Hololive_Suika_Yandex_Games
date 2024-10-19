using _Assets.Scripts.Services.Skins;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SkinSelectionGameState : IAsyncState
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly SkinService _skinService;

        public SkinSelectionGameState(UIStateMachine uiStateMachine, SkinService skinService)
        {
            _uiStateMachine = uiStateMachine;
            _skinService = skinService;
        }
        
        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchStateUI(UIStateType.Loading);
            _skinService.Init();
            await _uiStateMachine.SwitchStateUI(UIStateType.SkinSelection); 
        }

        public async UniTask Exit()
        {
           
        }
    }
}