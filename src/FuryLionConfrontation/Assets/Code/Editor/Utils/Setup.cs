using UnityEngine;

namespace Confrontation.Editor
{
	public static class Setup
	{
		public static Field Field(int height = 1, int width = 1)
			=> Create.Field(Create.CellPrefab(), Level(height, width));

		private static Level Level(int height = 1, int width = 1)
		{
			var level = Create.Level();
			level.Sizes = new Sizes(height, width);
			return level;
		}

		public static GameObject Cell() => Object.Instantiate(Create.CellPrefab()).gameObject;
	}
}