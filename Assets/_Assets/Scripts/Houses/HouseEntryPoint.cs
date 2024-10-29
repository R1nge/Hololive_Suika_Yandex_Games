using _Assets.Scripts.Houses.Interactables;
using Pathfinding;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Houses
{
    public class HouseEntryPoint : MonoBehaviour
    {
        [SerializeField] private int width, height;
        [SerializeField] private HouseCharacterController character;
        [SerializeField] private GridView gridView;
        [SerializeField] private InteractableController interactableController;
        [Inject] private GridService _gridService;
        [Inject] private IObjectResolver _objectResolver;

        private void Start()
        {
            var data = AstarPath.active.data;
            var newGrid = data.AddGraph(typeof(GridGraph)) as GridGraph;

            newGrid.is2D = true;
            newGrid.collision.use2D = true;
            newGrid.center = new Vector3(0f, 0, 0);
            newGrid.SetDimensions(width, height, 1);
            AstarPath.active.Scan();
            
            var player = _objectResolver.Instantiate(character, new Vector3(0, 0, 0), Quaternion.identity);
            interactableController.Init(player.GetComponent<AIPath>());
            var grid = _gridService.Generate(width, height, gridView);
            player.Init(grid);
        }
    }
}