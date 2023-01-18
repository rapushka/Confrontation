using System.Reflection;
using Confrontation;

namespace Tests.EditMode
{
	public static class ReflectionExtensions
	{
		private const string Cells = "_cells";

		public static Cell[,] GetCells(this Field @this) => @this.GetPrivate<Cell[,]>(Cells);

		public static T GetPrivate<T>(this object @this, string fieldName)
			=> @this.GetFieldValue<T>(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		public static T GetFieldValue<T>(this object @this, string fieldName, BindingFlags flags)
			=> (T)@this.GetType().GetField(fieldName, flags)!.GetValue(@this);
	}
}