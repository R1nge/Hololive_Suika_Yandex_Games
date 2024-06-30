using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services
{
    public class ComboPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] combos;
        [Inject] private ComboService _comboService;

        private void Start()
        {
            _comboService.OnComboChanged += SetCombo;
            HideCombos();
        }

        private void HideCombos()
        {
            for (int i = 0; i < combos.Length; i++)
            {
                combos[i].transform.DOScale(0, .1f);
            }
        
            
            for (int i = 0; i < combos.Length; i++)
            {
                combos[i].gameObject.SetActive(false);
            }
        }


        private void SetCombo(int combo, Vector3 position)
        {
            HideCombos();

            var index = (combo - 1) % combos.Length;
            if (index <= 0) index = 0;

            var currentCombo = combos[index];

            currentCombo.transform.localScale = Vector3.zero;

            currentCombo.transform.position = position;
            currentCombo.text = $"x{combo}";

            currentCombo.gameObject.SetActive(true);
            currentCombo.transform.DOScale(1, .1f);
        }
        
        private void OnDestroy() => _comboService.OnComboChanged -= SetCombo;
    }
}