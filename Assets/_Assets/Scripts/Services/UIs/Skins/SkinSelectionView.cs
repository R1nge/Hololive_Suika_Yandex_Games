using System;
using System.Collections.Generic;
using _Assets.Scripts.Services.Skins;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinSelectionView : MonoBehaviour
    {
        [SerializeField] private SkinView[] skins;
        [SerializeField] private LayerMask layerMask;
        [Inject] private SkinService _skinService;
        private int _firstSkinIndex = -1, _secondSkinIndex = -1;
        private readonly List<RaycastResult> _results = new(10);

        private void Start()
        {
            Init();
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
                Debug.Log("Ray");
                SelectSkinView();
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
            for (var i = 0; i < _results.Count; i++)
            {
                var result = _results[i];
                if (result.gameObject.layer == LayerMask.NameToLayer("SkinSelection")) // Check if the hit UI element is this element
                {
                    Debug.Log("Pointer is over " + gameObject.name);
                    var skinView = result.gameObject.GetComponent<SkinView>();
                    if (skinView != null)
                    {
                        if (_firstSkinIndex == -1)
                        {
                            _firstSkinIndex = skinView.SkinIndex;
                            Debug.Log(_firstSkinIndex);
                        }
                        else if (_secondSkinIndex == -1)
                        {
                            _secondSkinIndex = skinView.SkinIndex;
                            Debug.Log(_secondSkinIndex);
                            _skinService.Swap(_firstSkinIndex, _secondSkinIndex);
                            _firstSkinIndex = -1;
                            _secondSkinIndex = -1;
                            UpdateSprite();
                        }
                    }
                }
            }
        }
    }
}