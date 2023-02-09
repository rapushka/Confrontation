using System.Collections.Generic;

namespace Confrontation
{
	public static class HashSetExtensions
	{
		public static Player GetWithId(this HashSet<Player> @this, int id) => @this.GetLike(new Player(id));

		public static T GetLike<T>(this HashSet<T> @this, T template)
		{
			@this.TryGetValue(template, out var actualValue);

			return actualValue;
		}
	}
}