using Confrontation.Editor;
using UnityEngine;

namespace Confrontation.Tests
{
	public static class Create
	{
		public static Field Field(Cell prefab, int height = 1, int width = 1) => new(prefab, height, width);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();

		public static LevelEditor LevelEditor() => new();
	}
}