using System.Reflection;
using Confrontation;

public static class FieldExtensions
{
	private const string Cells = "_cells";

	public static Cell[,] GetCells(this Field @this)
		=> (Cell[,])typeof(Field).GetField(Cells, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(@this);
}