using System.Reflection;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class ReflectionExtensions
	{
		private const string CellsFieldName = "_cells";
		private const string RootFieldName = "_root";

		public static Cell[,] GetCells(this Field @this) => @this.GetPrivate<Cell[,]>(CellsFieldName);

		public static Transform GetRoot(this Field @this) => @this.GetPrivate<Transform>(RootFieldName);

		public static T GetPrivate<T>(this object @this, string fieldName)
			=> @this.GetFieldValue<T>(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		public static T GetFieldValue<T>(this object @this, string fieldName, BindingFlags flags)
			=> (T)@this.GetType().GetField(fieldName, flags)!.GetValue(@this);
	}
}