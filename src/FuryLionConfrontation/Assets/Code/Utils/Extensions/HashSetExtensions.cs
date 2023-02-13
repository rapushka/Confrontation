using System;
using System.Collections.Generic;

namespace Confrontation
{
	public static class HashSetExtensions
	{
		public static void RemoveById(this HashSet<Player> @this, int id) => @this.Remove(new Player(id));

		public static Player GetPlayerById(this HashSet<Player> @this, int id) => @this.GetValueLike(new Player(id));

		public static T GetValueLike<T>(this HashSet<T> @this, T template)
		{
			if (@this.TryGetValue(template, out var actualValue))
			{
				return actualValue;
			}

			throw new ArgumentException("HashSet don't contain object with hash like given");
		}
	}
}