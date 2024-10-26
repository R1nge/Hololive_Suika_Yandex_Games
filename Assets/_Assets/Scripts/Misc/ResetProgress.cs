using UnityEngine;
using YG;

namespace _Assets.Scripts.Misc
{
    public class ResetProgress : MonoBehaviour
    {
        public void ResetProgressYG()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
}