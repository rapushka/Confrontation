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

		public static Cell[,] GetCells(this Field @this) => @this.GetPrivate<Cell[,]>(CellsFieldName);

		public static Transform GetRoot(this Field @this) => @this.GetPrivate<Transform>(RootFieldName);

		public static void SetRegions(this Level @this, List<Region> value)
			=> @this.SetPrivateProperty(RegionsPropertyName, value);

		public static void SetVillageCoordinates(this Region @this, Coordinates value) 
			=> @this.SetPrivateProperty(VillageCoordinatesPropertyName, value);

		public static T GetPrivate<T>(this object @this, string fieldName)
			=> @this.GetFieldValue<T>(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		public static void SetPrivateProperty<T>(this object @this, string propertyName, T value)
			=> @this.SetPropertyValue(propertyName, value);

		private static T GetFieldValue<T>(this object @this, string fieldName, BindingFlags flags)
			=> (T)@this.GetType().GetField(fieldName, flags)!.GetValue(@this);

		private static void SetPropertyValue<T>(this object @this, string propertyName, T value)
			=> @this.GetType().GetProperty(propertyName)!.SetValue(@this, value);
	}
}