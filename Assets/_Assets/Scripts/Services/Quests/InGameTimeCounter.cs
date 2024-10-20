using System;
using Unity.VisualScripting;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Quests
{
    public class InGameTimeCounter : ITickable
    {
        private double _time;
        private bool _enabled;
        public event Action<double> OnTick;
        
        public double Time
        {
            get => _time;
            set => _time = value;
        }
        
        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;

        public void Tick()
        {
               if (_enabled)
               {
                   _time += UnityEngine.Time.deltaTime;
                   OnTick?.Invoke(_time);
               }
        }
    }
}