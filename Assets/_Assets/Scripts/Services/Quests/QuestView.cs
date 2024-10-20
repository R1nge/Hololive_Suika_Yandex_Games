using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI progress;
        [Inject] private QuestsService _questsService;

        private void Start()
        {
            title.text = _questsService.GetQuestProgress(QuestType.CompleteTimeRush).title;
            description.text = _questsService.GetQuestProgress(QuestType.CompleteTimeRush).description;
            progress.text = _questsService.GetQuestProgress(QuestType.CompleteTimeRush).progress + "/" + _questsService.GetQuestProgress(QuestType.CompleteTimeRush).maxProgress;
        }
    }
}