using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private const int FieldHeight = 20;
		private const int FieldWidth = 10;

		private readonly Cell _cellPrefab;
		private readonly Cell[,] _cells;
		private readonly Transform _root;

		[Inject]
		public Field(Cell cellPrefab)
		{
			_cellPrefab = cellPrefab;

			_cells = new Cell[FieldHeight, FieldWidth];
			_root = new GameObject("Cells Root").transform;
		}

		public void Initialize() => GenerateField();

		public void GenerateField() => _cells.Select(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = Object.Instantiate(_cellPrefab, _root);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition().AsTopDown();

			return cell;
		}
	}
}