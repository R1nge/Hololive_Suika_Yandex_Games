using System;
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
            aiPath.destination = new Vector3(cell.X, cell.Y, 0);
            Debug.Log(cell.X + " " + cell.Y);
            
        }
    }
}