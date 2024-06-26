namespace _Assets.Scripts.Services.StateMachine
{
    public enum GameStateType : byte
    {
        None = 0,
        Init = 1,
        Endless = 2,
        TimeRush = 3,
        GameOverEndless = 4,
        GameOverTimeRush = 5,
        ContinueEndless = 6,
        ContinueTimeRush = 8,
        Reset = 7,
    }
}