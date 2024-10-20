using System;
using System.Collections.Generic;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Skins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinListDescriptionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, descriptionText;
        [SerializeField] private Image previewImage;
        [SerializeField] private Button open;
        private readonly List<RaycastResult> _results = new(10);
        private int _index = -1;
        private bool _init;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private SkinService _skinService;

        private void Start()
        {
            open.onClick.AddListener(Init);
            _skinService.OnSetFirstSkin += Hide;
        }

        private void Init() => _init = true;

        private void Hide(int obj)
        {
            _init = false;
            _index = -1;
            nameText.text = string.Empty;
            descriptionText.text = string.Empty;
            previewImage.color = Color.clear;
        }

        private void Update()
        {
            if (_init)
            {
                Raycast();
            }
        }

        private async void Raycast()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                // Set the PointerEventData position to that of the mouse position
                position = Input.mousePosition
            };

            EventSystem.current.RaycastAll(pointerEventData, _results);

            if (_results.Count == 0)
            {
                return;
            }

            var result = _results[0];

            if (result.gameObject.layer == LayerMask.NameToLayer("SkinsList"))
            {
                if (result.gameObject.TryGetComponent(out SkinView skinView))
                {
                    if (skinView.SkinIndex == _index)
                    {
                        Debug.Log("Already selected");
                        return;
                    }

                    _index = skinView.SkinIndex;
                    var data = _configProvider.SuikaConfig.GetSkin(skinView.SkinIndex);
                    Set(data.Name.GetLocalizedString(), data.Description.GetLocalizedString(), await _configProvider.SuikaConfig.GetSprite(skinView.SkinIndex));
                }
            }
        }

        private void Set(string name, string description, Sprite sprite)
        {
            nameText.text = name;
            descriptionText.text = description;
            previewImage.sprite = sprite;
            previewImage.color = Color.white;
        }

        private void OnDestroy()
        {
            open.onClick.RemoveListener(Init);
            _skinService.OnSetFirstSkin -= Hide;
        }
    }
}