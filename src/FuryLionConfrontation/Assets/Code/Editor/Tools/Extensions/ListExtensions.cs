using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor
{
	public static class ListExtensions
	{
		public static void Resize<T>(this List<T> list, int newLength)
			where T : new()
		{
			Resize(list, newLength, new T());
		}

		public static void Resize<T>(this List<T> @this, int newLength, T template)
		{
			var oldLength = @this.Count;
			if (newLength < oldLength)
			{
				@this.RemoveRange(newLength, oldLength - newLength);
			}
			else if (newLength > oldLength)
			{
				@this.Capacity = newLength;
				@this.AddRange(Enumerable.Repeat(template, newLength - oldLength));
			}
		}
	}
}