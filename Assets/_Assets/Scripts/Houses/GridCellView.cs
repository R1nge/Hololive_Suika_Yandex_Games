using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class GridCellView : MonoBehaviour
    {
        [SerializeField]
        private GridCellData data;
        public GridCellData Data => data; 
        
        public void Init(GridCellData data)
        {
            this.data = data;
        }
    }
}