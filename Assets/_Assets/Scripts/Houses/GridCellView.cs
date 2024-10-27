using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class GridCellView : MonoBehaviour
    {
        private int _x, _y;
        private CellType _cellType;

        public void Init(int x, int y, CellType cellType)
        {
            _x = x;
            _y = y;
            _cellType = cellType;
        }
        
        public int X => _x;
        public int Y => _y;
        public CellType CellType => _cellType;
    }
}