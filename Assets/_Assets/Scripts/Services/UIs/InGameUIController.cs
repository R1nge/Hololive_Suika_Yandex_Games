using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameUIController
    {
        private readonly UIStateMachine _uiStateMachine;
        
        private InGameUIController(UIStateMachine uiStateMachine) => _uiStateMachine = uiStateMachine;

        public void Pause() => _uiStateMachine.SwitchState(UIStateType.Settings).Forget();
    }
}