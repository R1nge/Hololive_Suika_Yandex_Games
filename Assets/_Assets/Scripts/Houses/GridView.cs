using System;
using Pathfinding;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Houses
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private int width, height;
        [SerializeField] private GridCellView[,] cells;
        [Inject] private GridCellFactory _gridCellFactory;

        private void Awake()
        {
            cells = new GridCellView[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var type = CellType.Empty;

                    if (x == 0 && y == 0)
                    {
                        type = CellType.Door;
                    }

                    cells[x, y] = _gridCellFactory.Create(x, y, type, transform);
                    cells[x, y].gameObject.name = $"Cell_{x}_{y}";
                    cells[x, y].transform.position = new Vector3(x - width / 2, y - height / 2, 0);
                    cells[x, y].Init(x - width / 2, y - height / 2, CellType.Empty);
                }
            }

            //transform.position = new Vector3((-width + 1) / 2f, (-height + 1) / 2f, 0);
        }

        private void Start()
        {
            var data = AstarPath.active.data;
            var newGrid = data.AddGraph(typeof(GridGraph)) as GridGraph;

            newGrid.is2D = true;
            newGrid.collision.use2D = true;
            newGrid.center = new Vector3(-0.5f, -0.5f, 0);
            newGrid.SetDimensions(width, height, 1);
            AstarPath.active.Scan();
        }

        public GridCellView GetCellFromMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider == null)
            {
                return null;
            }

            if (hit.transform.TryGetComponent(out GridCellView cell))
            {
                return cell;
            }

            return null;
        }
    }
}