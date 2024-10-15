using System;
using _Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinListView : MonoBehaviour
    {
        [SerializeField] private SkinView skinView;
        [SerializeField] private Button open, close;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject child;
        [Inject] private ConfigProvider _configProvider;


        private void Start()
        {
            open.onClick.AddListener(Open);
            //close.onClick.AddListener(Close);
        }

        private async void Init()
        {
            for (int i = 0; i < _configProvider.SuikaConfig.SuikaSkins.Count; i++)
            {
                var skin = Instantiate(skinView, parent);
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
    }
}