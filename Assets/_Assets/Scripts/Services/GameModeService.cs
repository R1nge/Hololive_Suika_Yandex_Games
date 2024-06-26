namespace _Assets.Scripts.Services
{
    public class GameModeService
    {
        private GameMode _gameMode;
        
        public void SetGameMode(GameMode gameMode) => _gameMode = gameMode;
        
        public GameMode GetGameMode() => _gameMode;
        
        
        public enum GameMode : byte
        {
            None = 0,
            Endless = 1,
            TimeRush = 2
        }
    }
}