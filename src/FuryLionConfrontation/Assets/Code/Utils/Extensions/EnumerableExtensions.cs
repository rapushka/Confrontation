using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> @this, Action<T> @do)
		{
			foreach (var item in @this)
			{
				@do.Invoke(item);
			}
		}

		public static IEnumerable<T> OnlyUnique<T>(this IEnumerable<T> @this) => @this.Distinct();
	}
}