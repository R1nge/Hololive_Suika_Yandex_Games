using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class EndlessGameState : IAsyncState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly SuikasFactory _suikasFactory;

        public EndlessGameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, ContainerFactory containerFactory, PlayerFactory playerFactory, SuikasFactory suikasFactory)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _suikasFactory = suikasFactory;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Endless);
            _containerFactory.Create();
            var player = _playerFactory.Create();
            _suikasFactory.CreateKinematic(player.transform.position, player.transform);
        }

        public async UniTask Exit()
        {
        }
    }
}