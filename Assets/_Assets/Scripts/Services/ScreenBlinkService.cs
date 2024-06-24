using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services
{
    public class ScreenBlinkService : MonoBehaviour
    {
        [SerializeField] private Image blinkImage;
        [SerializeField] private Color blinkColor;
        private Color _blinkImageDefaultColor;
        [Inject] private GameOverTimer _gameOverTimer;
        private Sequence _sequence;
        private bool _blinking;

        private void Start()
        {
            _gameOverTimer.OnTimerStarted += StartBlink;
            _gameOverTimer.OnTimerStopped += StopBlink;
            _gameOverTimer.OnTimerEnded += StopBlink;

            _sequence = DOTween.Sequence();

            _sequence.Append(blinkImage.DOColor(blinkColor, .5f).SetEase(Ease.Flash));
            _sequence.AppendInterval(.25f);
            _sequence.Append(blinkImage.DOColor(_blinkImageDefaultColor, .5f).SetEase(Ease.Flash));
            _sequence.AppendInterval(.25f);
            _sequence.SetLoops(-1);
            _sequence.Pause();
        }

        private void StartBlink(float a, float b)
        {
            if (_blinking)
            {
                return;
            }

            _blinking = true;
            _sequence.Restart();
        }

        private void StopBlink(float a) => StopBlink();

        private void StopBlink()
        {
            if (!_blinking)
            {
                return;
            }

            _blinking = false;
            _sequence.Pause();
            blinkImage.color = _blinkImageDefaultColor;
        }

        private void OnDestroy()
        {
            _gameOverTimer.OnTimerStarted -= StartBlink;
            _gameOverTimer.OnTimerStopped -= StopBlink;
            _gameOverTimer.OnTimerEnded -= StopBlink;
        }
    }
}