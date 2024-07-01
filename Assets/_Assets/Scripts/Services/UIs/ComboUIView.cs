using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class ComboUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comboText;
        [Inject] private ComboService _comboService;

        private void Start() => _comboService.OnComboChanged += SetCombo;

        private void SetCombo(int combo, Vector3 position)
        {
            if (combo > 0)
            {
                comboText.text = $"x{combo}";
            }

            if (combo <= 0)
            {
                if (comboText.transform.localScale == Vector3.one)
                {
                    comboText.transform.DOScale(0, .1f);
                }
            }
            else
            {
                if (comboText.transform.localScale == Vector3.zero)
                {
                    comboText.transform.DOScale(1, .1f);
                }
            }
        }

        private void OnDestroy() => _comboService.OnComboChanged -= SetCombo;
    }
}