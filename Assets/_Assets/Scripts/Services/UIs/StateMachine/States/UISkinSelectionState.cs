using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UISkinSelectionState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UISkinSelectionState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter() => _ui = await _uiFactory.CreateUI(UIStateType.SkinSelection);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}