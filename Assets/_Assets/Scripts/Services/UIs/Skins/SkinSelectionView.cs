using System;
using _Assets.Scripts.Services.Skins;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinSelectionView : MonoBehaviour
    {
        [SerializeField] private Image[] skins;
        [Inject] private SkinService _skinService;

        private void Start()
        {
           Init(); 
        }

        public async void Init()
        {
            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                skins[i].sprite = await _skinService.GetSprite(i);
            }
        }
    }
}