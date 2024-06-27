using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace _Assets.Scripts.Services
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controls;
        private bool _enabled;
        private InputAction _moveAction;
        private readonly List<RaycastResult> _results = new(10);

        public bool Enabled(int fingerId = -1)
        {
            bool enabled = _enabled && !IsOverUI();
            return enabled;
        }

        private InputDevice _lastUsedDevice;
        public InputDevice LastUsedDevice => _lastUsedDevice;

        public event Action<InputDevice> OnDeviceChanged;
        public event Action<InputAction.CallbackContext> OnPause;
        public event Action<InputAction.CallbackContext> OnDrop;

        public Vector2 MoveVector
        {
            get
            {
                return LastUsedDevice.name == "Keyboard" || LastUsedDevice.name == "Gamepad"
                    ? _moveAction.ReadValue<Vector2>() * 0.1f
                    : _moveAction.ReadValue<Vector2>();
            }
        }

        private bool IsOverUI()
        {
            if (EventSystem.current == null)
            {
                Debug.LogWarning("No event system");
                return false;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    // Set the PointerEventData position to that of the mouse position
                    position = Input.mousePosition
                };

                // Raycast using the Graphics Raycaster and mouse click position
                EventSystem.current.RaycastAll(pointerEventData, _results);

                // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                for (var i = 0; i < _results.Count; i++)
                {
                    var result = _results[i];
                    if (result.gameObject.layer == LayerMask.NameToLayer("ClickableUI")) // Check if the hit UI element is this element
                    {
                        Debug.Log("Pointer is over " + gameObject.name);
                        _results.Clear();
                        return true;
                    }
                }
            }

            return false;
        }

        public void Init()
        {
            //Since it's a singleton and init is called only once the game starts, we can forget about unsubscribing.
            InputSystem.onEvent += OnInputSystemEvent;
            InputSystem.onDeviceChange += OnDeviceChange;
            controls.FindActionMap("Game").FindAction("Pause").performed += PauseInputCallback;
            controls.FindActionMap("Game").FindAction("Drop").performed += DropCallback;
            _moveAction = controls.FindActionMap("Game").FindAction("Move");
            controls.Enable();
        }

        private void DropCallback(InputAction.CallbackContext callback)
        {
            if (!Enabled())
            {
                return;
            }

            OnDrop?.Invoke(callback);
        }

        private void PauseInputCallback(InputAction.CallbackContext callback)
        {
            if (!Enabled())
            {
                return;
            }

            OnPause?.Invoke(callback);
        }

        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (Equals(_lastUsedDevice, device))
                return;

            _lastUsedDevice = device;
            OnDeviceChanged?.Invoke(_lastUsedDevice);
        }


        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (_lastUsedDevice == device)
                return;

            // Some devices like to spam events like crazy.
            // Example: PS4 controller on PC keeps triggering events without meaningful change.
            var eventType = eventPtr.type;
            if (eventType == StateEvent.Type)
            {
                // Go through the changed controls in the event and look for ones actuated
                // above a magnitude of a little above zero.
                if (!eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.0001f).Any())
                    return;
            }


            _lastUsedDevice = device;
            OnDeviceChanged?.Invoke(_lastUsedDevice);
        }
    }
}