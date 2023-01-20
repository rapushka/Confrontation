using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Field Field(Cell prefab, Level level) => new(prefab, level);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();

		public static LevelEditor LevelEditor() => new();

		public static Level Level() => ScriptableObject.CreateInstance<Level>();
	}
}