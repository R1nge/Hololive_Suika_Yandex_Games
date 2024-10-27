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

                    var selectedCell = cells[x, y];
                    selectedCell = _gridCellFactory.Create(x, y, type, transform);
                    selectedCell.gameObject.name = $"Cell_{x}_{y}";
                    var xPos = x - width / 2;
                    var yPos = y - height / 2;
                    selectedCell.transform.position = new Vector3(xPos, yPos, 0);
                    var data = selectedCell.Data;
                    data.X = xPos;
                    data.Y = yPos;
                    selectedCell.Init(data);
                }
            }
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