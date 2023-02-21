using System.Collections.Generic;

namespace Confrontation
{
	public static class HashSetExtensions
	{
		public static void RemoveById(this HashSet<Player> @this, int id) => @this.Remove(new Player(id));

		public static Player GetPlayerByIdOrDefault(this HashSet<Player> @this, int id)
			=> @this.GetValueLikeOrDefault(new Player(id));

		public static T GetValueLikeOrDefault<T>(this HashSet<T> @this, T template)
			where T : class
			=> @this.TryGetValue(template, out var actualValue) ? actualValue : null;

		public static void AddRange<T>(this HashSet<T> @this, IEnumerable<T> range)
		{
			foreach (var item in range)
			{
				@this.Add(item);
			}
		}
	}
}