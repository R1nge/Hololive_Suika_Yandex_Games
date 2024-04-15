using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Factories;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using PlayerInput = _Assets.Scripts.Services.PlayerInput;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float horizontalLimit = 3f;
        [Inject] private CoroutineRunner _coroutineRunner;
        [Inject] private SuikasFactory _suikasFactory;
        [Inject] private PlayerInput _playerInput;
        private PlayerDrop _playerDrop;
        private PlayerMovement _playerMovement;
        private bool _dropQueued;

        public void Init()
        {
            _playerDrop = new PlayerDrop(_coroutineRunner, _suikasFactory, transform);
            _playerInput.OnDrop += Drop;
            _playerMovement = new PlayerMovement(_playerInput, transform, horizontalLimit);
        }

        private void Drop(InputAction.CallbackContext obj)
        {
            _dropQueued = true;
        }

        private void Update()
        {
            if (_playerInput.Enabled())
            {
                _playerMovement.Tick();

                if (_dropQueued)
                {
                    if (_playerDrop.TryDrop())
                    {
                        _dropQueued = false;
                    }
                }
            }
            else
            {
                _dropQueued = false;
            }
        }

        public void SpawnSuika() => _playerDrop.SpawnSuika();
        
        public void SpawnContinue() => _playerDrop.SpawnContinue();

        private void OnDestroy() => _playerInput.OnDrop -= Drop;
    }
}