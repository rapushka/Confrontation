using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Setup
	{
		public static Field FieldWithRegion(int count = 1, int column = 1)
		{
			var regions = Regions(count, column);
			var level = Level(regions);
			return Field(level);
		}

		public static Field Field(int height = 1, int width = 1) => Field(Level(height, width));

		public static Field Field(Level level) => Create.Field(Create.CellPrefab(), Create.VillagePrefab(), level);

		public static Level Level(List<Region> regions, int height = 1, int width = 1)
		{
			var level = Level(height, width);
			level.SetRegions(regions);
			return level;
		}

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

		private static List<Region> Regions(int count = 1, int column = 1)
			=> new(Enumerable.Range(1, count).Select((i) => Region(i, column)));

		public static Region Region(int row = 1, int column = 1)
		{
			var region = Create.Region();
			region.SetVillageCoordinates(new Coordinates(row, column));
			return region;
		}
	}
}