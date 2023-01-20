using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly Cell _cellPrefab;
		private readonly Level _level;
		private readonly Cell[,] _cells;
		private readonly Transform _root;
		private readonly Village _villagePrefab;

		private Lookup<Coordinates, Coordinates> _regions;

		[Inject]
		public Field(Cell cellPrefab, Village villagePrefab, Level level)
		{
			_cellPrefab = cellPrefab;
			_villagePrefab = villagePrefab;
			_level = level;

			_cells = new Cell[_level.Sizes.Height, _level.Sizes.Width];
			_root = new GameObject("Cells Root").transform;
		}

		public void Initialize() => GenerateField();

		public void GenerateField()
		{
			RegionsToLookup();

			_cells.Select(CreateHexagon);

			DivideByRegions();
		}

		public void ToVillage(Cell cell)
		{
			var village = Object.Instantiate(original: _villagePrefab, parent: cell.transform);
			cell.Building = village;
		}

		private void RegionsToLookup()
			=> _regions = (Lookup<Coordinates, Coordinates>)ToEnumerableOfTuples()
				.ToLookup((x) => x.VillageCoordinates, (x) => x.Coordinates);

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
				}
			}
		}

		private IEnumerable<(Coordinates VillageCoordinates, Coordinates Coordinates)> ToEnumerableOfTuples()
			=> _level.Regions
			         .SelectMany((r) => r.CellsInRegion, (r, c) => (r.VillageCoordinates, Coordinates: c));

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = Object.Instantiate(original: _cellPrefab, parent: _root);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition().AsTopDown();
			if (IsVillage(cell))
			{
				ToVillage(cell);
			}

			return cell;
		}

		private bool IsVillage(Cell cell) => _level.Regions.Any((r) => r.VillageCoordinates == cell.Coordinates);
	}
}