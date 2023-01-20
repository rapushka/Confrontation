using System.Collections.Generic;
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

		public static void AddRegion(this Field @this, Coordinates villageCoordinates)
			=> @this.GetPrivateField<Level>(LevelFieldName)
			        .GetPropertyValue<List<Region>>(RegionsPropertyName)
			        .Add(NewRegion(villageCoordinates));

		private static Region NewRegion(Coordinates villageCoordinates)
		{
			var region = new Region();
			region.SetPrivateProperty("VillageCoordinates", villageCoordinates);
			return region;
		}

		public static Cell[,] GetCells(this Field @this) => @this.GetPrivateField<Cell[,]>(CellsFieldName);

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