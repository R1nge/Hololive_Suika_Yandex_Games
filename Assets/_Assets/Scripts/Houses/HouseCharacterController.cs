using System.Linq;
using Pathfinding;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class HouseCharacterController : MonoBehaviour
    {
        [SerializeField] private GridView gridView;
        [SerializeField] private AIPath aiPath;

        private void Awake()
        {
            transform.parent = gridView.transform;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cell = gridView.GetCellFromMousePosition();
                if (cell == null) return;
                MoveTo(cell);
            }
        }

        private void MoveTo(GridCellView cell)
        {
            var closestPosition = cell.ClosePositions
                .OrderBy(transform1 => Vector2.Distance(transform.position, transform1.position)).FirstOrDefault();
            if (closestPosition != null)
            {
                aiPath.destination = closestPosition.position;
            }
            else
            {
                aiPath.destination = new Vector3(cell.Data.X, cell.Data.Y);
            }
        }
    }
}