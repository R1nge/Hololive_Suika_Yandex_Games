using System;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class ComboService : ITickable
    {
        private int _combo;
        public event Action<int> OnComboChanged;
        private bool _isComboActive;
        private const float ResetTime = 2f;
        private float _currentTime;
        
        public int Combo => _combo;
        public bool IsComboActive => _isComboActive;

        public void AddCombo()
        {
            _currentTime = ResetTime;
            _isComboActive = true;
            _combo++;
            OnComboChanged?.Invoke(_combo);
        }

        public void Tick()
        {
            if (!_isComboActive) return;

            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
            }
            else
            {
                ResetCombo();
            }
        }

        public void ResetCombo()
        {
            _currentTime = ResetTime;
            _isComboActive = false;
            _combo = 0;
            OnComboChanged?.Invoke(_combo);
        }
    }
}