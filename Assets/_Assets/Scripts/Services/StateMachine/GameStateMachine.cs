using System.Collections.Generic;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStateMachine : GenericAsyncStateMachine<GameStateType, IAsyncState>
    {
        private GameStateMachine(GameStatesFactory gameStatesFactory)
        {
            States = new Dictionary<GameStateType, IAsyncState>
            {
                { GameStateType.Init, gameStatesFactory.CreateAsyncState(GameStateType.Init, this) },
                { GameStateType.Endless, gameStatesFactory.CreateAsyncState(GameStateType.Endless, this) },
                { GameStateType.GameOverEndless, gameStatesFactory.CreateAsyncState(GameStateType.GameOverEndless, this) },
                { GameStateType.ContinueEndless, gameStatesFactory.CreateAsyncState(GameStateType.ContinueEndless, this) },
                { GameStateType.TimeRush, gameStatesFactory.CreateAsyncState(GameStateType.TimeRush, this) },
                { GameStateType.GameOverTimeRush, gameStatesFactory.CreateAsyncState(GameStateType.GameOverTimeRush, this) },
                { GameStateType.ContinueTimeRush, gameStatesFactory.CreateAsyncState(GameStateType.ContinueTimeRush, this) },
                { GameStateType.Continue , gameStatesFactory.CreateAsyncState(GameStateType.Continue, this) }
            };
        }
    }
}