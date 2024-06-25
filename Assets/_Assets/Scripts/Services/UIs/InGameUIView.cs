using System;
using _Assets.Scripts.Configs;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image nextSuikaImage;
        [SerializeField] private Button pauseButton;
        [Inject] private InGameUIController _inGameUIController;
        [Inject] private RandomNumberGenerator _randomNumberGenerator;
        [Inject] private ScoreService _scoreService;
        [Inject] private ConfigProvider _configProvider;

        private void Awake()
        {
            pauseButton.onClick.AddListener(Pause);
        }

        private void Start()
        {
            _scoreService.OnScoreChanged += ScoreChanged;
            SuikaPicked(0, 0, _randomNumberGenerator.Next);
            _randomNumberGenerator.OnSuikaPicked += SuikaPicked;
            ScoreChanged(_scoreService.Score);
        }
        
        private void ScoreChanged(int score) => scoreText.text = score.ToString();

        private async void SuikaPicked(int previous, int current, int next)
        {
            var sprite = await _configProvider.SuikaConfig.GetSprite(_randomNumberGenerator.Next);
            nextSuikaImage.sprite = sprite;
        }

        private void Pause() => _inGameUIController.Pause();

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(Pause);
            _scoreService.OnScoreChanged -= ScoreChanged;
            _randomNumberGenerator.OnSuikaPicked -= SuikaPicked;
        }
    }
}