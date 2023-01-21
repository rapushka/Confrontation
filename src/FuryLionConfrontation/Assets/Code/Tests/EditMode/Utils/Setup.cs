using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Setup
	{
		public static Field Field(int height = 1, int width = 1) => Field(Level(height, width));

		public static Field Field(Level level) => Create.Field(Create.CellPrefab(), Create.VillagePrefab(), level);

		public static Level Level(List<Village.Data> regions, int height = 1, int width = 1)
		{
			var level = Level(height, width);
			level.SetRegions(regions);
			return level;
		}

		public static Level Level(int height = 1, int width = 1)
		{
			var level = Create.Level();
			level.SetSizes(new Sizes(height, width));
			return level;
		}

		public static Cell Cell(int row = 1, int column = 1)
		{
			var cell = Object.Instantiate(Create.CellPrefab());
			cell.Coordinates = new Coordinates(row, column);
			return cell;
		}

		public static LevelEditor LevelEditor(int height = 0, int width = 0)
		{
			var levelEditor = Create.LevelEditor();
			levelEditor.GenerateField(height, width);
			return levelEditor;
		}

		public static List<Village.Data> Regions(int count = 1, int column = 1)
			=> new(Enumerable.Range(1, count).Select((i) => Region(i, column)));

		public static Village.Data Region(int row = 1, int column = 1)
		{
			var region = Create.Region();
			region.SetVillageCoordinates(new Coordinates(row, column));
			return region;
		}

		public static ResourcesService ResourcesService(Cell cellPrefab, Village villagePrefab, Level level)
		{
			var resources = ScriptableObject.CreateInstance<ResourcesService>();
			resources.SetPrivateProperty("CellPrefab", cellPrefab);
			resources.SetPrivateProperty("VillagePrefab", villagePrefab);
			resources.SetPrivateProperty("CurrentLevel", level);
			return resources;
		}
	}
}