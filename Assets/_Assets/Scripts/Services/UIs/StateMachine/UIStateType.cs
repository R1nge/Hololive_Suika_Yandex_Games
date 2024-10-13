namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public enum UIStateType : byte
    {
        None = 0,
        Loading = 1,
        MainMenu = 2,
        Settings = 3,
        GameModeSelection = 6,
        Endless = 4,
        TimeRush = 8,
        Pause = 7,
        GameOverEndless = 5,
        GameOverTimeRush = 9,
        SkinSelection = 10
    }
}