using _Assets.Scripts.Services.Skins;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinView : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private GameObject selection;
        [Inject] private SkinService _skinService;
        private int _skinIndex;
        public int SkinIndex => _skinIndex;

        public void UpdateSprite(Sprite sprite)
        {
            skinImage.sprite = sprite;
        }

        public void Init(Sprite sprite, int skinIndex)
        {
            skinImage.sprite = sprite;
            _skinIndex = skinIndex;
        }

        public void Select()
        {
            selection.SetActive(_skinService.Selected(_skinIndex));
        }
    }
}