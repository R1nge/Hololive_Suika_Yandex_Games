using System;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    [Serializable]
    public struct GridCellData
    {
        public int X;
        public int Y;
        public CellType CellType;
        public bool IsMovable;
        public Vector2Int InteractionOffset;

        public GridCellData(int x, int y, CellType cellType, bool isMovable, Vector2Int interactionOffset)
        {
            X = x;
            Y = y;
            CellType = cellType;
            IsMovable = isMovable;
            InteractionOffset = interactionOffset;
        }
    }
}