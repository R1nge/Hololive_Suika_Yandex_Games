using System;
using System.Collections.Generic;
using _Assets.Scripts.Services.Skins;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinSelectionView : MonoBehaviour
    {
        [SerializeField] private SkinView[] skins;
        [SerializeField] private Button backButton;
        [SerializeField] private LayerMask layerMask;
        [Inject] private SkinService _skinService;
        [Inject] private UIStateMachine _uiStateMachine;
        private int _firstSkinIndex = -1, _secondSkinIndex = -1;
        private readonly List<RaycastResult> _results = new(10);

        private Transform _firstSkinTransform;
        private Vector3 _firstSkinPosition;

        private void Start()
        {
            backButton.onClick.AddListener(Back);
            Init();
        }

        private void Back()
        {
            _uiStateMachine.SwitchToPreviousState().Forget();
        }

        public async void Init()
        {
            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                skins[i].Init(await _skinService.GetSprite(i), i);
            }
        }

        private async void UpdateSprite()
        {
            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                skins[i].UpdateSprite(await _skinService.GetSprite(i));
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectSkinView();
            }

            if (_firstSkinTransform != null)
            {
                Vector3 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 localPosition = _firstSkinTransform.root.InverseTransformPoint(worldPosition);
                _firstSkinTransform.localPosition = localPosition;
            }
        }

        private void SelectSkinView()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                // Set the PointerEventData position to that of the mouse position
                position = Input.mousePosition
            };

            EventSystem.current.RaycastAll(pointerEventData, _results);

            // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            bool found = false;
            for (var i = 0; i < _results.Count; i++)
            {
                var result = _results[i];
                if (result.gameObject.layer ==
                    LayerMask.NameToLayer("SkinSelection")) // Check if the hit UI element is this element
                {
                    Debug.Log("Pointer is over " + gameObject.name);
                    var skinView = result.gameObject.GetComponent<SkinView>();

                    if (skinView != null && skinView.SkinIndex != _firstSkinIndex)
                    {
                        found = true;
                        if (_firstSkinIndex == -1)
                        {
                            _firstSkinTransform = skinView.transform;
                            _firstSkinIndex = skinView.SkinIndex;
                            _firstSkinPosition = skinView.transform.position;
                            _firstSkinTransform.SetAsLastSibling();
                            Debug.Log($"First: {_firstSkinIndex}");
                        }
                        else if (_secondSkinIndex == -1)
                        {
                            _secondSkinIndex = skinView.SkinIndex;
                            Debug.Log($"Second: {_secondSkinIndex}");
                            _skinService.Swap(_firstSkinIndex, _secondSkinIndex);
                            _firstSkinIndex = -1;
                            _secondSkinIndex = -1;
                            _firstSkinTransform.position = _firstSkinPosition;
                            _firstSkinTransform = null;
                            UpdateSprite();
                        }
                    }
                }
            }

            if (!found)
            {
                if (_firstSkinTransform != null)
                {
                    _firstSkinTransform.position = _firstSkinPosition;
                    _firstSkinTransform = null;
                }

                _firstSkinIndex = -1;
                _secondSkinIndex = -1;
            }
        }
    }
}