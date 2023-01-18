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

			for (var x = 0; x < cellsCount; x++)
			{
				for (var z = 0; z < cellsCount; z++)
				{
					var cell = Object.Instantiate(_cellPrefab);
					cell.Coordinates = new Vector2Int(x, z);

					var position = cell.transform.position;
					position.x = z % 2 == 0 ? x : x + 0.5f;
					position.y = 0;
					position.z = z * (3 / (2 * Mathf.Sqrt(3)));

					cell.transform.position = position;
				}
			}
		}
	}
}