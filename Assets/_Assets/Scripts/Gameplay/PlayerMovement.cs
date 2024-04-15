using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = _Assets.Scripts.Services.PlayerInput;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerMovement
    {
        private readonly PlayerInput _playerInput;
        private readonly Transform _transform;
        private readonly float _horizontalLimit;

        public PlayerMovement(PlayerInput playerInput, Transform transform, float horizontalLimit)
        {
            _playerInput = playerInput;
            _transform = transform;
            _horizontalLimit = horizontalLimit;
        }

        public void Tick()
        {
            if (!_playerInput.Enabled()) return;

            var input = Mathf.Clamp(_playerInput.MoveVector.x, -_horizontalLimit, _horizontalLimit);
            var newPosition = _transform.position + new Vector3(input, 0, 0);

            //Probably, should be in the input class and
            //return a vector3 depending on the device being used
            //but I don't care
            if (_playerInput.LastUsedDevice.name == "Mouse")
            {
                input = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Mouse.current.position.value).x, -_horizontalLimit, _horizontalLimit);
                newPosition = new Vector3(input, _transform.position.y, _transform.position.z);
            }
            else if (_playerInput.LastUsedDevice.name == "Touchscreen")
            {
                input = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.value).x, -_horizontalLimit, _horizontalLimit);
                newPosition = new Vector3(input, _transform.position.y, _transform.position.z);
            }
            
            var clamped = Mathf.Clamp(newPosition.x, -_horizontalLimit, _horizontalLimit);
            _transform.position = new Vector3(clamped, _transform.position.y, _transform.position.z);
        }
    }
}