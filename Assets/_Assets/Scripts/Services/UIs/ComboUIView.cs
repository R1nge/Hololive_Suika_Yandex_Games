using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class ComboUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comboText;
        [Inject] private ComboService _comboService;

        private void Start()
        {
            _comboService.OnComboChanged += SetCombo;
        }

        private void SetCombo(int combo) => comboText.text = combo.ToString();

        private void OnDestroy() => _comboService.OnComboChanged -= SetCombo;
    }
}