using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly IResourcesService _resources;
		private readonly IAssetsService _assets;

		private readonly Cell[,] _cells;

		[Inject]
		public Field(IResourcesService resources, IAssetsService assets)
		{
			_resources = resources;
			_assets = assets;

			var levelSizes = resources.CurrentLevel.Sizes;
			_cells = new Cell[levelSizes.Height, levelSizes.Width];
		}

		public void Initialize() => GenerateField();

		public void GenerateField()
		{
			_cells.SetForEach(CreateHexagon);
			DivideIntoRegions();
		}

		private void DivideIntoRegions()
		{
			foreach (var region in _resources.CurrentLevel.Regions)
			{
				var villageCoordinates = region.Coordinates;
				var village = CreateVillage(_cells[villageCoordinates.Row, villageCoordinates.Column]);
				foreach (var cell in region.Cells.Select((cc) => _cells[cc.Row, cc.Column]))
				{
					village.CellsInRegion.Add(cell);
					cell.ToRedRegion();
				}
			}
		}

		private Village CreateVillage(Cell cell)
		{
			var village = _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform);
			cell.Building = village;
			return village;
		}

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = _assets.Instantiate(original: _resources.CellPrefab, InstantiateGroup.Cells);
			cell.Coordinates = coordinates;

			return cell;
		}
	}
}