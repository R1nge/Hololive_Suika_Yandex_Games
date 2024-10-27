using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    [CreateAssetMenu(fileName = "HouseItemsConfig", menuName = "Configs/House/Items Config", order = 0)]
    public class HouseItemsConfig : SerializedScriptableObject
    {
        [OdinSerialize]
        private Dictionary<GridCellView, CellType> houseItems = new();
        
        public GridCellView GetHouseItem(CellType cellType)
        {
            return houseItems.FirstOrDefault(x => x.Value == cellType).Key;
        }
    }
}