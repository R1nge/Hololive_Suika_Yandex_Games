using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionText;

        private void Awake() => versionText.text = $"v{Application.version}";
    }
}