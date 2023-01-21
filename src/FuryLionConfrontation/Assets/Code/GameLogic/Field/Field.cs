using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly IResourcesService _resources;
		private readonly IAssetsService _assets;
		
		private readonly Cell[,] _cells;

		private ILookup<Coordinates, Coordinates> _regions;

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
			_regions = _resources.CurrentLevel.RegionsAsLookup();
			_cells.SetForEach(CreateHexagon);
			DivideByRegions();
		}

		public void CreateVillage(Cell cell) 
			=> cell.Building = _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = _assets.Instantiate(original: _resources.CellPrefab, InstantiateGroup.Cells);
			cell.Coordinates = coordinates;
			if (IsVillage(cell))
			{
				CreateVillage(cell);
			}

			return cell;
		}

		private void DivideByRegions()
		{
			foreach (var region in _regions)
			{
				var coordinatesOfVillage = region.Key;
				var village = (Village)_cells[coordinatesOfVillage.Row, coordinatesOfVillage.Column].Building;
				foreach (var coordinatesOfCell in region)
				{
					var cell = _cells[coordinatesOfCell.Row, coordinatesOfCell.Column];

					village.CellsInRegion.Add(cell);
					cell.ToRedRegion();
				}
			}
		}

		private bool IsVillage(Cell cell) => _regions.Contains(key: cell.Coordinates);
	}
}