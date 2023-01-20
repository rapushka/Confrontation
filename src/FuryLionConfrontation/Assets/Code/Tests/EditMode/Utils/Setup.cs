using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Setup
	{
		public static Field Field(int height = 1, int width = 1)
			=> Create.Field(Create.CellPrefab(), Create.VillagePrefab(), Level(height, width));

		public static Level Level(int height = 1, int width = 1)
		{
			var level = Create.Level();
			level.Sizes = new Sizes(height, width);
			return level;
		}

		public static GameObject Cell() => Object.Instantiate(Create.CellPrefab()).gameObject;

		public static LevelEditor LevelEditor(int height = 0, int width = 0)
		{
			var levelEditor = Create.LevelEditor();
			levelEditor.GenerateField(height, width);
			return levelEditor;
		}
	}
}