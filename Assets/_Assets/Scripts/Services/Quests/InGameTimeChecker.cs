using System;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Quests
{
    public class InGameTimeChecker : IInitializable, IDisposable
    {
        private readonly InGameTimeCounter inGameTimeCounter;
        private readonly QuestsService questsService;

        private InGameTimeChecker(InGameTimeCounter inGameTimeCounter, QuestsService questsService)
        {
            this.inGameTimeCounter = inGameTimeCounter;
            this.questsService = questsService;
        }


        public void Initialize()
        {
           inGameTimeCounter.OnTick += CheckTime; 
        }

        private void CheckTime(double time)
        {
            if (time >= 60 * questsService.GetQuest(QuestType.Playtime).maxProgress)
            {
                questsService.CompleteQuest(QuestType.Playtime);
                inGameTimeCounter.Disable();
            }
            else
            {
                questsService.SetQuestProgress(QuestType.Playtime, (int) time / 60);
            }
        }

        public void Dispose()
        {
            if (inGameTimeCounter != null)
            {
                inGameTimeCounter.OnTick -= CheckTime;
            }
        }
    }
}