using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Assets.Scripts.Misc
{
    public class Boot : MonoBehaviour
    {
        private void Start() => SceneManager.LoadSceneAsync("GridTest", LoadSceneMode.Single);
    }
}