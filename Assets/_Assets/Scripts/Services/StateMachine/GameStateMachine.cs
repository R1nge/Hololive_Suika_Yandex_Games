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
                { GameStateType.Reset, gameStatesFactory.CreateAsyncState(GameStateType.Reset, this) },
            };
        }
    }
}