using _Assets.Scripts.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Houses
{
    public class GridCellFactory
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;

        private GridCellFactory(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }
        
        public GridCellView Create(int x, int y, CellType cellType, Transform parent)
        {
            var item = _configProvider.HouseItemsConfig.GetHouseItem(cellType);
            var cell = _objectResolver.Instantiate(item, new Vector3(x, y, 0), Quaternion.identity, parent);
            return cell;
        }
    }
}