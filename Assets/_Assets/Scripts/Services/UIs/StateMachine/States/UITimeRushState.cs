﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UITimeRushState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UITimeRushState(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public async UniTask Enter()
        {
            _ui = await _uiFactory.CreateUI(UIStateType.TimeRush);
        }

        public async UniTask Exit()
        {
            Object.Destroy(_ui);
        }
    }
}