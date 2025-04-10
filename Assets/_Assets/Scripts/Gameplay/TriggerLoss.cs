﻿using System.Collections.Generic;
using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class TriggerLoss : MonoBehaviour
    {
        [SerializeField] private float timeBeforeTrigger = 2f;
        private float _time;
        private List<Suika> _collidedSuikas = new(10);
        private bool _ended;
        [Inject] private GameOverTimer _gameOverTimer;

        private void Start() => _gameOverTimer.OnTimerEnded += TimerEnded;

        private void TimerEnded() => _ended = true;

        private void Update()
        {
            if (_collidedSuikas.Count > 0 && !_ended)
            {
                _time += Time.deltaTime;

                if (_time >= timeBeforeTrigger)
                {
                    _gameOverTimer.StartTimer();
                }
            }
            else
            {
                _gameOverTimer.StopTimer();
                _time = 0;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                if (!_collidedSuikas.Contains(suika))
                {
                    if (suika.HasLanded)
                    {
                        _collidedSuikas.Add(suika);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                if (_collidedSuikas.Contains(suika))
                {
                    _collidedSuikas.Remove(suika);
                }
            }
        }

        private void OnDestroy() => _gameOverTimer.OnTimerEnded -= TimerEnded;
    }
}