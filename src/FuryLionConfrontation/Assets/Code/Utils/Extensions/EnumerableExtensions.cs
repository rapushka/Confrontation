using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

		public static T PickRandom<T>(this IEnumerable<T> @this)
		{
			var array = @this as T[] ?? @this.ToArray();
			return array.PickRandom();
		}

		public static T PickRandom<T>(this T[] @this)
		{
			var randomId = UnityEngine.Random.Range(0, @this.Length);
			return @this[randomId];
		}
	}
}