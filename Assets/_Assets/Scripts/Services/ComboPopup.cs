using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services
{
    public class ComboPopup : MonoBehaviour
    {
        [SerializeField] private Color[] predefinedColors;
        [SerializeField] private TextMeshProUGUI[] combos;
        [Inject] private ComboService _comboService;

        private void Start()
        {
            _comboService.OnComboChanged += SetCombo;

            for (int i = 0; i < combos.Length; i++)
            {
                combos[i].gameObject.SetActive(false);
            }
        }


        private void SetCombo(int combo, Vector3 position)
        {
            if (combo <= 0) return;

            var index = (combo - 1) % combos.Length;
            if (index <= 0) index = 0;

            var currentCombo = combos[index];

            currentCombo.transform.localScale = Vector3.zero;

            currentCombo.transform.position = position;
            currentCombo.text = $"x{combo}";
            currentCombo.color = GetRandomColor();

            currentCombo.gameObject.SetActive(true);
            currentCombo.transform.DOScale(1, .25f).onComplete = () => currentCombo.transform.DOScale(0,.25f);
        }

        private Color GetRandomColor()
        {
            return predefinedColors[_comboService.Combo % predefinedColors.Length];
        }

        private void OnDestroy() => _comboService.OnComboChanged -= SetCombo;
    }
}