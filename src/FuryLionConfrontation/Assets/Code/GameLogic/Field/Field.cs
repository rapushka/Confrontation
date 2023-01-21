using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly Cell _cellPrefab;
		private readonly Village _villagePrefab;

		private readonly Level _level;
		private readonly Cell[,] _cells;

		private readonly Transform _root;
		private readonly IAssetsService _assets;
		
		private ILookup<Coordinates, Coordinates> _regions;

		[Inject]
		public Field(Level level, Cell cellPrefab, Village villagePrefab, IAssetsService assets)
		{
			_level = level;
			_cellPrefab = cellPrefab;
			_villagePrefab = villagePrefab;
			_assets = assets;

			_cells = new Cell[_level.Sizes.Height, _level.Sizes.Width];
			_root = _assets.Instantiate("Cells Root").transform;
		}

		public void Initialize() => GenerateField();

		public void GenerateField()
		{
			RegionsToLookup();
			_cells.Set(CreateHexagon);
			DivideByRegions();
		}

		public void ToVillage(Cell cell)
		{
			var village = _assets.Instantiate(original: _villagePrefab, parent: cell.transform);
			cell.Building = village;
		}

		private void RegionsToLookup()
			=> _regions = _level.Regions
			                    .SelectMany((r) => r.Cells, (r, c) => (VillageCoordinates: r.Coordinates, Cell: c))
			                    .ToLookup((x) => x.VillageCoordinates, (x) => x.Cell);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = _assets.Instantiate(original: _cellPrefab, parent: _root);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition().AsTopDown();
			if (IsVillage(cell))
			{
				ToVillage(cell);
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