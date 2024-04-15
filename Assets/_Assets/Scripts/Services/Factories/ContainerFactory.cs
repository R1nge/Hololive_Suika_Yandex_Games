using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class ContainerFactory : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private Transform spawnPoint;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private ResetService _resetService;

        public void Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            _resetService.SetContainer(container);
        }
    }
}