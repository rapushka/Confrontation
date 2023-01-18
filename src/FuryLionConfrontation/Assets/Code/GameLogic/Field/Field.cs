using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code
{
	public class Field : IInitializable
	{
		private const int FieldWidth = 10;
		private const int FieldHeight = 20;

		private readonly Cell _cellPrefab;
		private readonly Cell[,] _cells;

		[Inject]
		public Field(Cell cellPrefab)
		{
			_cellPrefab = cellPrefab;

			_cells = new Cell[FieldWidth, FieldHeight];
		}

		public void Initialize() => GenerateField();

		private void GenerateField() => _cells.Select(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Vector2Int(i, j);
			var cell = Object.Instantiate(_cellPrefab);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition();

			return cell;
		}
	}
}