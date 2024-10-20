using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class DDOL : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}