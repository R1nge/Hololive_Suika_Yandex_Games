using System;
using _Assets.Scripts.Configs;
using UnityEngine;

namespace _Assets.Scripts.Services
{
    public class ScoreService
    {
        private readonly ComboService _comboService;
        private readonly ConfigProvider _configProvider;

        private ScoreService(ComboService comboService, ConfigProvider configProvider)
        {
            _comboService = comboService;
            _configProvider = configProvider;
        }

        public event Action<int> OnScoreChanged;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        private int _score;

        public void SetScore(int score) => Score = score;

        public void AddScore(int index)
        {
            //Previous + level (index) + points? 
            var currentLevel = index;
            var previousPoints = _configProvider.SuikaConfig.GetPoints(index - 1);
            var totalPoints = currentLevel + previousPoints;

            if (_comboService.IsComboActive)
            {
                totalPoints *= _comboService.Combo;
            }


            if (totalPoints <= 0)
            {
                Debug.LogError("SCORE SERVICE: Score must be positive");
                return;
            }

            Score += totalPoints;
        }

        public void ResetScore() => Score = 0;
    }
}