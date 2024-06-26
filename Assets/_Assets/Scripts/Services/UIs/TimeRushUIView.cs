using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class TimeRushUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timer;
        [Inject] private TimeRushTimer _timeRushTimer;

        private void Start() => _timeRushTimer.OnTimerChanged += TimerChanged;

        private void TimerChanged(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            
            if (minutes < 1)
            {
                if (seconds <= 9)
                {
                    timer.text = $"0:0{seconds}";
                }
                else
                {
                    timer.text = $"0:{seconds}";
                }
            }
            else
            {
                if (seconds <= 9)
                {
                    timer.text = $"{minutes}:0{seconds}";
                }
                else
                {
                    timer.text = $"{minutes}:{seconds}";
                }
            }
        }

        private void OnDestroy() => _timeRushTimer.OnTimerChanged -= TimerChanged;
    }
}