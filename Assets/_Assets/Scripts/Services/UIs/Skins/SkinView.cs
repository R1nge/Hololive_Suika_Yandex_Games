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
        [SerializeField] private Outline outline;
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
            outline.enabled = false;
        }

        public void Select()
        {
            selection.SetActive(_skinService.Selected(_skinIndex));
        }

        public void Preview()
        {
            if (outline.enabled)
            {
                return;
            }

            outline.enabled = true;
        }

        public void UnPreview()
        {
            if (!outline.enabled)
            {
                return;
            }

            outline.enabled = false;
        }
    }
}