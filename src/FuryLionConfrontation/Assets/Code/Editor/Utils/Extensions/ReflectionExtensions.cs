using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class ReflectionExtensions
	{
		private const string CellsFieldName = "_cells";
		private const string RootFieldName = "_root";
		private const string RegionsPropertyName = "Regions";
		private const string VillageCoordinatesPropertyName = "VillageCoordinates";
		private const string LevelFieldName = "_level";

		public static void AddRegion(this Field @this, Region region) => @this.GetRegions().Add(region);

		public static List<Region> GetRegions(this Field @this)
			=> @this.GetLevel().GetPropertyValue<List<Region>>(RegionsPropertyName);

		public static Level GetLevel(this Field @this) => @this.GetPrivateField<Level>(LevelFieldName);

		public static IEnumerable<Village> GetVillages(this Field @this)
			=> @this.GetCells().Select((c) => c.Building).OfType<Village>();

		public static IEnumerable<Cell> GetCells(this Field @this)
			=> @this.GetPrivateField<Cell[,]>(CellsFieldName).Cast<Cell>();

		public static Transform GetRoot(this Field @this) => @this.GetPrivateField<Transform>(RootFieldName);

		public static void SetRegions(this Level @this, List<Region> value)
			=> @this.SetPrivateProperty(RegionsPropertyName, value);

		public static void SetVillageCoordinates(this Region @this, Coordinates value)
			=> @this.SetPrivateProperty(VillageCoordinatesPropertyName, value);

		public static T GetPrivateField<T>(this object @this, string fieldName)
			=> @this.GetFieldValue<T>(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		public static void SetPrivateProperty<T>(this object @this, string propertyName, T value)
			=> @this.SetPropertyValue(propertyName, value);

		private static T GetFieldValue<T>(this object @this, string fieldName, BindingFlags flags)
			=> (T)@this.GetType().GetField(fieldName, flags)!.GetValue(@this);

		private static T GetPropertyValue<T>(this object @this, string fieldName)
			=> (T)@this.GetType().GetProperty(fieldName)!.GetValue(@this);

		private static void SetPropertyValue<T>(this object @this, string propertyName, T value)
			=> @this.GetType().GetProperty(propertyName)!.SetValue(@this, value);
	}
}