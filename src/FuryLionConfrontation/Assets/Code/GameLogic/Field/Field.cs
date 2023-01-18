using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class Field : IInitializable
	{
		private const int Height = 20;
		private const int Width = 10;

		private readonly Cell _cellPrefab;
		private readonly Cell[,] _cells;

		[Inject]
		public Field(Cell cellPrefab, int height = Height, int width = Width)
		{
			_cellPrefab = cellPrefab;

			_cells = new Cell[height, width];
			Root = new GameObject("Cells Root").transform;
		}

		public Transform Root { get; }

		public void Initialize() => GenerateField();

		public void GenerateField() => _cells.Select(CreateHexagon);

		private Cell CreateHexagon(int i, int j)
		{
			var coordinates = new Coordinates(i, j);
			var cell = Object.Instantiate(_cellPrefab, Root);
			cell.Coordinates = coordinates;
			cell.transform.position = coordinates.CalculatePosition().AsTopDown();

			return cell;
		}
	}
}