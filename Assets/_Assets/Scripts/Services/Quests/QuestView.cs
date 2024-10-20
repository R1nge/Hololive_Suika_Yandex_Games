using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Slider progress;
        [SerializeField] private QuestType questType;
        [Inject] private QuestsService _questsService;

        private void Start()
        {
            title.text = _questsService.GetQuest(questType).title;
            description.text = _questsService.GetQuest(questType).description;
            progress.maxValue = _questsService.GetQuest(questType).maxProgress;
            progress.value = _questsService.GetQuest(questType).progress;
        }
    }
}