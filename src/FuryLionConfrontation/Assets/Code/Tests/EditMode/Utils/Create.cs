using UnityEngine;

namespace Confrontation.Tests
{
	public static class Create
	{
		public static Field Field(Cell cellPrefab, int height = 20, int width = 10)
			=> new(cellPrefab, height, width);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();
	}
}