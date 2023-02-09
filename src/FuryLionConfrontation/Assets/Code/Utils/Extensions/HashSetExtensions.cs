using System.Collections.Generic;

namespace Confrontation
{
	public static class HashSetExtensions
	{
		public static void RemoveById(this HashSet<Player> @this, int id) => @this.Remove(new Player(id));
	}
}