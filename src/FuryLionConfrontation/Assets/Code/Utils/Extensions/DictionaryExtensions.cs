using System.Collections.Generic;

namespace Confrontation
{
	public static class DictionaryExtensions
	{
		public static void EnsureAdded<TKey, TValue>(this Dictionary<TKey, TValue> @this, TKey key, TValue value)
		{
			if (@this.ContainsKey(key) == false)
			{
				@this.Add(key, value);
			}
		}
	}
}