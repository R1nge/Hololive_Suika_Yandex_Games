﻿
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
        
        public int highScoreEndless;
        public int highScoreTimeRush;

        public float musicVolume;
        public float vfxVolume;

        public ContinueData continueData;

        public int coins;
        
        public SavesYG()
        {
            musicVolume = .1f;
            vfxVolume = .1f;
        }
    }
}
