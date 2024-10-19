using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Skins;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinListView : MonoBehaviour
    {
        [SerializeField] private SkinView skinView;
        [SerializeField] private LayerMask listSkinsMask;
        [SerializeField] private Button open, close;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject child;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private Image background;
        private SkinView[] _skins;
        private bool _init;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private SkinService _skinService;


        private void Start()
        {
            open.onClick.AddListener(Open);
            //close.onClick.AddListener(Close);
            _skinService.OnSet += RebuildLayout;
            _skinService.OnSetFirstSkin += Hide;
        }

        private void RebuildLayout()
        {
            var cellSize = grid.cellSize;
            grid.cellSize = cellSize + Vector2.one;
            grid.cellSize = cellSize;
        }

        private async void Init()
        {
            if (_init)
            {
                return;
            }

            _skins = new SkinView[_configProvider.SuikaConfig.SuikaSkins.Count];
            for (int i = 0; i < _configProvider.SuikaConfig.SuikaSkins.Count; i++)
            {
                var skin = _objectResolver.Instantiate(skinView, parent);
                _skins[i] = skin;
                skin.gameObject.layer = LayerMask.NameToLayer("SkinsList");
            }

            for (int i = 0; i < _skins.Length; i++)
            {
                var sprite = await _configProvider.SuikaConfig.GetSprite(i);
                _skins[i].Init(sprite, i);
                _skins[i].UpdateSprite(sprite);
                _skins[i].Select();
            }

            _init = true;
        }

        public void Open()
        {
            Init();
            background.enabled = true;

            for (int i = 0; i < _skins.Length; i++)
            {
                _skins[i].gameObject.SetActive(true);
            }

            child.SetActive(true);
        }

        public void Hide(int indexToIgnore)
        {
            //child.SetActive(false);
            if (_init)
            {
                background.enabled = false;

                for (int i = 0; i < _skins.Length; i++)
                {
                    if (i != indexToIgnore)
                    {
                        _skins[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _skinService.OnSet -= RebuildLayout;
            _skinService.OnSetFirstSkin -= Hide;
        }
    }
}