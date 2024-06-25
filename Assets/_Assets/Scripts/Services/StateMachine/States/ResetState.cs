using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetState : IAsyncState
    {
        private readonly ResetService _resetService;

        public ResetState(ResetService resetService)
        {
            _resetService = resetService;
        }

        public async UniTask Enter()
        {
            _resetService.Reset();
        }

        public async UniTask Exit()
        {
        }
    }
}