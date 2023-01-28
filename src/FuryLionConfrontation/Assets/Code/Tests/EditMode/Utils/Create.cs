using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Create
	{
		public static Coordinates Coordinates(int row = 0, int column = 0) => new(row, column);

		public static ResourcesService ResourcesService()
			=> Resources.Load<ResourcesService>("ScriptableObjects/Resources");

		public static Cell Cell() => new GameObject().AddComponent<Cell>();
	}

	public static class Setup
	{
		private const string FieldColor = "_color";

		public static Cell Cell()
		{
			var cell = Create.Cell();
			var regionColor = cell.gameObject.AddComponent<RegionColor>();
			cell.SetPrivateField(FieldColor, regionColor);
			return cell;
		}
	}
}