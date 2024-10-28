using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class GridCellView : MonoBehaviour
    {
        [SerializeField]
        private GridCellData data;
        public GridCellData Data => data;

        [SerializeField] private Transform[] closePositions;
        public Transform[] ClosePositions => closePositions;
        
        public void Init(GridCellData data)
        {
            this.data = data;
        }
    }
}