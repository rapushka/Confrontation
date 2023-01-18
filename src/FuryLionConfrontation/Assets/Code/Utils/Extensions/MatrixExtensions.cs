using System;

namespace Code
{
	public static class MatrixExtensions
	{
		public static void Select<T>(this T[,] @this, Func<int, int, T> action)
		{
			for (var i = 0; i < @this.GetLength(0); i++)
			{
				for (var j = 0; j < @this.GetLength(1); j++)
				{
					@this[i, j] = action.Invoke(i, j);
				}
			}
		}

		public static void DoubleFor<T>(this T[,] @this, Action<T, int, int> action)
		{
			for (var i = 0; i < @this.GetLength(0); i++)
			{
				for (var j = 0; j < @this.GetLength(1); j++)
				{
					action.Invoke(@this[i, j], i, j);
				}
			}
		}
	}
}