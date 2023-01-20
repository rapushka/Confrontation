using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private readonly Cell _cellPrefab;
		private readonly Cell[,] _cells;
		private readonly Transform _root;

		[Inject] public Field(Cell cellPrefab, Coordinates sizes) : this(cellPrefab, sizes.Row, sizes.Column) { }

		public Field(Cell cellPrefab, int height, int width)
		{
			_cellPrefab = cellPrefab;

			_cells = new Cell[height, width];
			_root = new GameObject("Cells Root").transform;
		}

		public void Initialize() => GenerateField();

		public void GenerateField() => _cells.Select(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = Object.Instantiate(original: _cellPrefab, parent: _root);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition().AsTopDown();

			return cell;
		}
	}
}