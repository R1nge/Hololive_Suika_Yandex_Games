using System.Collections.Generic;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStateMachine : GenericAsyncStateMachine<UIStateType, IAsyncState>
    {
        private UIStateMachine(UIStatesFactory uiStatesFactory)
        {
            States = new Dictionary<UIStateType, IAsyncState>
            {
                { UIStateType.Loading, uiStatesFactory.CreateState(UIStateType.Loading, this) },
                { UIStateType.MainMenu, uiStatesFactory.CreateState(UIStateType.MainMenu, this) },
                { UIStateType.GameModeSelection, uiStatesFactory.CreateState(UIStateType.GameModeSelection, this) },
                { UIStateType.Endless, uiStatesFactory.CreateState(UIStateType.Endless, this) },
                { UIStateType.Settings, uiStatesFactory.CreateState(UIStateType.Settings, this) },
                { UIStateType.Pause, uiStatesFactory.CreateState(UIStateType.Pause, this) },
                { UIStateType.GameOverEndless, uiStatesFactory.CreateState(UIStateType.GameOverEndless, this) },
                { UIStateType.GameOverTimeRush, uiStatesFactory.CreateState(UIStateType.GameOverTimeRush, this) },
                { UIStateType.TimeRush, uiStatesFactory.CreateState(UIStateType.TimeRush, this) }
            };
        }
    }
}