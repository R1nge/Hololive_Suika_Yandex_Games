using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class LoadingIconRotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject loadingIcon;

        private void LateUpdate() => loadingIcon.transform.Rotate(new Vector3(0, 0, 90) * (speed * Time.deltaTime));
    }
}