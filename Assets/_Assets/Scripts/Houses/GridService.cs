using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Houses
{
    public class GridService
    {
        private readonly IObjectResolver objectResolver;
        private GridCellData[,] _data;

        private GridService(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        public GridView Generate(int width, int height, GridView gridView)
        {
            _data = new GridCellData[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var type = CellType.Empty;

                    if (x == 0 && y == 0)
                    {
                        type = CellType.Door;
                    }

                    var selectedCell = _data[x, y];
                    var xPos = x - width / 2;
                    var yPos = y - height / 2;
                    _data[x, y] = new GridCellData(xPos, yPos, type, selectedCell.IsMovable);
                }
            }


            var instance = objectResolver.Instantiate(gridView);
            instance.Init(_data);
            return instance;
        }
    }
}