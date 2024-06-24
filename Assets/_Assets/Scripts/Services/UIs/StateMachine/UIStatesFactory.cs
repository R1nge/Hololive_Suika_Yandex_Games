using System;
using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly UIFactory _uiFactory;

        private UIStatesFactory(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public IAsyncState CreateState(UIStateType uiStateType, UIStateMachine uiStateMachine)
        {
            switch (uiStateType)
            {
                case UIStateType.Loading:
                    return new UILoadingState(_uiFactory);
                case UIStateType.MainMenu:
                    return new UIMainMenuState(_uiFactory);
                case UIStateType.GameModeSelection:
                    return new UIGameModeSelectionState(_uiFactory);
                case UIStateType.Settings:
                    return new UISettingsState(_uiFactory);
                case UIStateType.Endless:
                    return new UIEndlessState(_uiFactory);
               case UIStateType.Pause:
                    return new UIPauseState(_uiFactory);
                case UIStateType.GameOverEndless:
                    return new UIEndlessGameOverState(_uiFactory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }
        }
    }
}