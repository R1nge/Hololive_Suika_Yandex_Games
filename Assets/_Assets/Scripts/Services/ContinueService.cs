using System.Collections.Generic;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace _Assets.Scripts.Services
{
    public class ContinueService
    {
        private ContinueData _continueData;
        private readonly List<Suika> _suikas = new();
        private readonly AudioService _audioService;
        private readonly SuikasFactory _suikasFactory;
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly ScoreService _scoreService;

        public bool HasData => _continueData != null;

        private ContinueService(AudioService audioService, SuikasFactory suikasFactory,
            RandomNumberGenerator randomNumberGenerator, ScoreService scoreService)
        {
            _audioService = audioService;
            _suikasFactory = suikasFactory;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
        }

        public async UniTask Continue()
        {
            await Load();

            Debug.LogError($"Song index: {_continueData.SongIndex}");

            for (int i = 0; i < _continueData.SuikasContinueData.Count; i++)
            {
                var position = new Vector3(_continueData.SuikasContinueData[i].PositionX,
                    _continueData.SuikasContinueData[i].PositionY, 0);
                _suikasFactory.CreateContinue(_continueData.SuikasContinueData[i].Index, position);
            }

            _randomNumberGenerator.SetCurrent(_continueData.CurrentSuikaIndex);
            _randomNumberGenerator.SetNext(_continueData.NextSuikaIndex);

//            _timeRushTimer.CurrentTime = _continueData.TimeRushTime;

        }

        public void UpdateScore()
        {
            _scoreService.SetScore(_continueData.Score);
        }

        public async UniTask Load()
        {
            _continueData = YandexGame.savesData.continueData;
        }

        public void AddSuika(Suika suika) => _suikas.Add(suika);

        public void RemoveSuika(Suika suika) => _suikas.Remove(suika);

        public async UniTask Save()
        {
            _continueData = new ContinueData(_audioService.LastSongIndex, new List<ContinueData.SuikaContinueData>(),
                _randomNumberGenerator.Current, _randomNumberGenerator.Next, _scoreService.Score, 99);
            // _timeRushTimer.CurrentTime);

            _continueData.SuikasContinueData = new List<ContinueData.SuikaContinueData>(_suikas.Count);
            for (int i = 0; i < _suikas.Count; i++)
            {
                if (_suikas[i] == null)
                {
                    continue;
                }

                var index = _suikas[i].Index;
                var position = _suikas[i].transform.position;
                _continueData.SuikasContinueData.Add(new ContinueData.SuikaContinueData(index, position.x, position.y));
            }

            _continueData.SongIndex = _audioService.LastSongIndex;

            _continueData.CurrentSuikaIndex = _randomNumberGenerator.Current;
            _continueData.NextSuikaIndex = _randomNumberGenerator.Next;

            _continueData.Score = _scoreService.Score;

            //_continueData.TimeRushTime = _timeRushTimer.CurrentTime;
            YandexGame.savesData.continueData = _continueData;
            YandexGame.SaveProgress();
        }

        public void DeleteContinueData()
        {
            _continueData = null;
            YandexGame.savesData.continueData = null;
            YandexGame.SaveProgress();
        }
    }
}