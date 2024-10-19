using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Skins;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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
        [Inject] private ConfigProvider _configProvider;
        [Inject] private SkinService _skinService;


        private void Start()
        {
            open.onClick.AddListener(Open);
            //close.onClick.AddListener(Close);
            _skinService.OnSet += RebuildLayout;
        }

        private void RebuildLayout()
        {
            var cellSize = grid.cellSize;
            grid.cellSize = cellSize + Vector2.one;
            grid.cellSize = cellSize;
        }

        private async void Init()
        {
            for (int i = 0; i < _configProvider.SuikaConfig.SuikaSkins.Count; i++)
            {
                var skin = Instantiate(skinView, parent);
                skin.gameObject.layer = LayerMask.NameToLayer("SkinsList");
                var sprite = await _configProvider.SuikaConfig.GetSprite(i);
                skin.Init(sprite, i);
                skin.UpdateSprite(sprite);
            }
        }

        public void Open()
        {
            child.SetActive(true);
            Init();
        }

        public void Close() => child.SetActive(false);

        private void OnDestroy()
        {
            _skinService.OnSet -= RebuildLayout;
        }
    }
}