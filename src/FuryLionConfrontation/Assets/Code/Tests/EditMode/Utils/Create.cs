using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Field Field(Cell cellPrefab, Village villagePrefab, Level level)
			=> new(level, cellPrefab, villagePrefab);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();

		public static Village VillagePrefab() => new GameObject().AddComponent<Village>();

		public static LevelEditor LevelEditor() => new();

		public static Level Level() => ScriptableObject.CreateInstance<Level>();
		public static Region Region() => new();
	}
}