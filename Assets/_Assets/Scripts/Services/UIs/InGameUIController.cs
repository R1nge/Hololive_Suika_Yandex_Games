using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameUIController
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerInput _playerInput;

        private InGameUIController(UIStateMachine uiStateMachine, PlayerInput playerInput)
        {
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
        }

        public void Pause()
        {
            _playerInput.Disable();
            _uiStateMachine.SwitchStateUI(UIStateType.Pause).Forget();
        }
    }
}