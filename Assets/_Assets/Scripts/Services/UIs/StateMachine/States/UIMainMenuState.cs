using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIMainMenuState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;
        
        public UIMainMenuState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter() => _ui = await _uiFactory.CreateUI(UIStateType.MainMenu);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}