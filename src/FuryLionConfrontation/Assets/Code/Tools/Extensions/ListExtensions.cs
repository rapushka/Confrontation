using System;
using System.Collections.Generic;

namespace Confrontation
{
	public static class ListExtensions
	{
		public static void RemoveIf<T>(this List<T> @this, Func<T, bool> predicate)
		{
			for (var i = 0; i < @this.Count; i++)
			{
				if (predicate.Invoke(@this[i]))
				{
					@this.RemoveAt(i);
					i--;
				}
			}
		}
	}
}