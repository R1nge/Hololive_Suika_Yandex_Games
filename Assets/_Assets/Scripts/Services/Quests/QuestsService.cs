using System;
using System.Linq;
using UnityEngine;
using YG;

namespace _Assets.Scripts.Services.Quests
{
    public class QuestsService : MonoBehaviour
    {
        [SerializeField] private Quest[] quests;

        public Quest GetQuest(QuestType questType)
        {
            return quests.First(quest => quest.progressData.questType == questType);
        }

        public void CompleteQuest(QuestType questType)
        {
            var quest = quests.Where(quest => quest.progressData.questType == questType).Select(quest => quest).First();
            quest.Complete();
            Save();
        }

        public void SetQuestProgress(QuestType questType, int progress)
        {
            var quest = quests.Where(quest => quest.progressData.questType == questType).Select(quest => quest).First();
            quest.progress = progress;
            Save();
        }

        public void ResetQuest(QuestType questType)
        {
            var quest = quests.Where(quest => quest.progressData.questType == questType).Select(quest => quest).First();
            quest.Reset();
            Save();
        }

        public void Save()
        {
            YandexGame.savesData.quests = quests;
        }

        public void Load()
        {
            if (YandexGame.savesData.quests != null)
            {
                quests = YandexGame.savesData.quests;
            }
        }
    }

    public enum QuestType : byte
    {
        None = 0,
        CompleteTimeRush = 1,
        CompleteEndless = 2,
        DailyStreak = 3,
        Playtime = 4
    }

    [Serializable]
    public struct QuestProgressData
    {
        public QuestType questType;
        public bool isCompleted;
    }

    [Serializable]
    public class Quest
    {
        public string title;
        public string description;
        public int progress, maxProgress;
        public QuestProgressData progressData;

        public Quest(string title, string description, int progress, int maxProgress, QuestProgressData progressData)
        {
            this.title = title;
            this.description = description;
            this.progress = progress;
            this.maxProgress = maxProgress;
            this.progressData = progressData;
        }

        public void Reset()
        {
            progressData.isCompleted = false;
        }

        public void Complete()
        {
            progressData.isCompleted = true;
            progress = maxProgress;
        }
    }
}