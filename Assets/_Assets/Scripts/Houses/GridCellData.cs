using System;

namespace _Assets.Scripts.Houses
{
    [Serializable]
    public struct GridCellData
    {
        public int X;
        public int Y;
        public CellType CellType;
        public bool IsMovable;

        public GridCellData(int x, int y, CellType cellType, bool isMovable)
        {
            X = x;
            Y = y;
            CellType = cellType;
            IsMovable = isMovable;
        }
    }
}