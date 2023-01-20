using Confrontation.Editor;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Field Field(Cell prefab, Level level) => new(prefab, level);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();

		public static LevelEditor LevelEditor() => new();

		public static Level Level() => Object.Instantiate(LevelScriptableObject());

		public static Level LevelScriptableObject() => Resources.Load<Level>("ScriptableObjects/Level");
	}
}