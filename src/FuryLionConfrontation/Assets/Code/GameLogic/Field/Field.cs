using UnityEngine;
using Zenject;

namespace Code
{
	public class Field : IInitializable
	{
		private readonly Cell _cellPrefab;

		public Field(Cell cellPrefab) => _cellPrefab = cellPrefab;

		public void Initialize() => GenerateField();

		private void GenerateField()
		{
			const int cellsCount = 20;

			for (var i = 0; i < cellsCount; i++)
			{
				for (var j = 0; j < cellsCount; j++)
				{
					CreateHexagonAt(i, j);
				}
			}
		}

		private void CreateHexagonAt(int i, int j)
		{
			var cell = Object.Instantiate(_cellPrefab);
			cell.Coordinates = new Vector2Int(i, j);

			cell.transform.position = CalculatePosition(i, j);
		}

		private static Vector3 CalculatePosition(int x, int z)
			=> new()
			{
				x = x + HorizontalOffset(z),
				z = z * VerticalDistance(),
			};

		private static float HorizontalOffset(int z) => IsEven(z) ? 0 : 0.5f;

		private static float VerticalDistance() => 3 / (2 * Mathf.Sqrt(3));

		private static bool IsEven(int z) => z % 2 == 0;
	}
}