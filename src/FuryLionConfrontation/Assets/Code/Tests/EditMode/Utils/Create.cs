using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Field Field(Cell cellPrefab, Village villagePrefab, Level level)
			=> new(level, cellPrefab, villagePrefab, new AssetsService());

		public static Cell CellPrefab() => Resources.Load<Cell>("Prefabs/Cell");

		public static Village VillagePrefab() => new GameObject().AddComponent<Village>();

		public static LevelEditor LevelEditor() => new();

		public static Level Level() => ScriptableObject.CreateInstance<Level>();
		public static Village.Data Region() => new();
		public static Coordinates Coordinates(int row = 0, int column = 0) => new(row, column);
	}
}