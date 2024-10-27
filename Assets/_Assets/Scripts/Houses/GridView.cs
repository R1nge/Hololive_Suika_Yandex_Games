using System;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GridCellView cellViewPrefab;
        [SerializeField] private int width, height;
        [SerializeField] private GridCellView[,] cells;

        private void Awake()
        {
            cells = new GridCellView[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = Instantiate(cellViewPrefab, transform);
                    cells[x, y].gameObject.name = $"Cell_{x}_{y}";
                    cells[x, y].transform.position = new Vector3(x - width / 2, y - height / 2, 0);
                    cells[x, y].Init(x - width / 2, y - height / 2, CellType.Empty);
                }
            }

            //transform.position = new Vector3((-width + 1) / 2f, (-height + 1) / 2f, 0);
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