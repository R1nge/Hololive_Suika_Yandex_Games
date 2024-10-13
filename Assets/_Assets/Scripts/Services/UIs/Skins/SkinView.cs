using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinView : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        private int _skinIndex;
        public int SkinIndex => _skinIndex;
        
        public void UpdateSprite(Sprite sprite)
        {
            skinImage.sprite = sprite;
        }

        public void Init(Sprite sprite,int skinIndex)
        {
            skinImage.sprite = sprite; 
            _skinIndex = skinIndex;
        }
    }
}