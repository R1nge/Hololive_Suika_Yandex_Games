
using _Assets.Scripts.Services;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        //
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        //
        public int highScoreEndless;
        public int highScoreTimeRush;

        public ContinueData continueData;
        
        public SavesYG()
        {
            openLevels[1] = true;
        }
    }
}
