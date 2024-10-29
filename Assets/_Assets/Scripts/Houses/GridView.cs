using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Houses
{
    public class GridView : MonoBehaviour
    {
        private GridCellView[,] cells;
        [Inject] private GridCellFactory _gridCellFactory;
        [Inject] private GridService _gridService;

        public void Init(GridCellData[,] data)
        {
            Generate(data);
        }

        private void Generate(GridCellData[,] data)
        {
            cells = new GridCellView[data.GetLength(0), data.GetLength(1)];
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    var cell = _gridCellFactory.Create(x, y, data[x, y].CellType, transform);
                    cells[x, y] = cell;
                    var xPos = x - data.GetLength(0) / 2;
                    var yPos = y - data.GetLength(1) / 2;
                    cells[x,y].transform.position = new Vector3(xPos, yPos, 0);
                    cell.Init(data[x, y]);
                }
            }
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