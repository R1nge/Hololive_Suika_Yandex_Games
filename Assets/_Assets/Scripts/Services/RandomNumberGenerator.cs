using System;
using _Assets.Scripts.Configs;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Services
{
    public class RandomNumberGenerator
    {
        private int _previous, _current, _previousNext, _next;
        private bool _isFirstCall = true;
        private readonly ConfigProvider _configProvider;
        
        private RandomNumberGenerator(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public event Action<int, int, int> OnSuikaPicked;

        public int Previous => _previous;
        public int Current => _current;
        public int Next => _next;

        public int PickRandomSuika()
        {
            if (_isFirstCall)
            {
                _isFirstCall = false;
                _current = Random.Range(0, 5);
                _next = Random.Range(0, 5);
            }

            var chance = Random.Range(0f, 1f);
            var next = Random.Range(0, 5);

            if (chance <= _configProvider.GameConfig.SuikaDropChances[next])
            {
                _previous = _current;
                _current = _next;
                _next = next;

                OnSuikaPicked?.Invoke(_previous, _current, _next);
            }
            else
            {
                return PickRandomSuika();
            }


            return _current;
        }

        public void SetCurrent(int currentIndex) => _current = currentIndex;

        public void SetNext(int nextIndex) => _next = nextIndex;
    }
}