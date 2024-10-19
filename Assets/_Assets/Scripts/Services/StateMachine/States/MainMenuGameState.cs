using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class MainMenuGameState : IAsyncState
    {
        private readonly UIStateMachine _uiStateMachine;

        public MainMenuGameState(UIStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchStateUI(UIStateType.MainMenu);
        }

        public async UniTask Exit()
        {
        }
    }
}