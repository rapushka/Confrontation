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

		public static T[] AsArray<T>(this IEnumerable<T> @this) => @this as T[] ?? @this.ToArray();

		public static bool TryPickRandom<T>(this IEnumerable<T> @this, out T result)
			where T : class
			=> @this.AsArray().TryPickRandom(out result);

		public static T PickRandom<T>(this IEnumerable<T> @this) => @this.AsArray().PickRandom();

		public static bool TryPickRandom<T>(this T[] @this, out T result)
			where T : class
		{
			result = @this.Any() ? @this.PickRandom() : null;
			return result is not null;
		}

		public static T PickRandom<T>(this T[] @this)
		{
			var randomId = UnityEngine.Random.Range(0, @this.Length);
			return @this[randomId];
		}
	}
}