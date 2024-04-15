using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services
{
    public class UICanvasAnimation : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image background;
        private static readonly int Blur = Shader.PropertyToID("_Size");

        public async UniTask Play(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.FadeIn:
                    if (background != null)
                    {
                        DOVirtual.Float(0, 4, duration, value => background.material.SetFloat(Blur, value));
                    }

                    await canvasGroup.DOFade(1, duration).AsyncWaitForCompletion();
                    break;
                case AnimationType.FadeOut:
                    if (background != null)
                    {
                        DOVirtual.Float(4, 0, duration, value => background.material.SetFloat(Blur, value));
                    }

                    await canvasGroup.DOFade(0, duration).AsyncWaitForCompletion();
                    break;
            }
        }

        private void OnDestroy() => canvasGroup.DOKill();
    }

    public enum AnimationType
    {
        FadeIn,
        FadeOut
    }
}