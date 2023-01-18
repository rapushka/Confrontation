using UnityEngine;
using Zenject;

namespace Code
{
	public class Field : IInitializable
	{
		private const int FieldSizes = 20;

		private readonly Cell _cellPrefab;
		private readonly Cell[,] _cells;

		[Inject]
		public Field(Cell cellPrefab)
		{
			_cellPrefab = cellPrefab;

			_cells = new Cell[FieldSizes, FieldSizes];
		}

		public void Initialize() => GenerateField();

		private void GenerateField()
		{
			for (var i = 0; i < FieldSizes; i++)
			{
				for (var j = 0; j < FieldSizes; j++)
				{
					CreateHexagonFor(new Vector2Int(i, j));
				}
			}
		}

		private void CreateHexagonFor(Vector2Int coordinates)
		{
			var cell = Object.Instantiate(_cellPrefab);
			cell.Coordinates = coordinates;
			_cells[coordinates.x, coordinates.y] = cell;

			cell.transform.position = coordinates.CalculatePosition();
		}
	}
}