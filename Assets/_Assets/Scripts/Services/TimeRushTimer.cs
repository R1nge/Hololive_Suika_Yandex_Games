using System;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class TimeRushTimer : IInitializable, ITickable
    {
        private readonly float _maxTime = 60f * 3f;
        private float _currentTime;
        private bool _enabled;
        public float CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnTimerChanged?.Invoke(_currentTime);
            }
        }

        public event Action<float> OnTimerStarted; 
        public event Action<float> OnTimerChanged;
        public event Action<float> OnTimerEnded;

        public void Initialize() => _currentTime = _maxTime;

        public void Start()
        {
            CurrentTime = _maxTime;
            _enabled = true;
            OnTimerStarted?.Invoke(CurrentTime);
        }

        public void Tick()
        {
            if (_enabled)
            {
                CurrentTime -= Time.deltaTime;

                if (CurrentTime <= 1)
                {
                    Reset();
                    OnTimerChanged?.Invoke(0f);
                    OnTimerEnded?.Invoke(0f);
                }
            }    
        }

        public void Pause() => _enabled = false;

        public void Resume() => _enabled = true;


        public void Reset()
        {
            CurrentTime = _maxTime;
            _enabled = false;
        }
    }
}