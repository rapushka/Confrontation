using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public static class PairedHashSetExtensions
	{
		public static int CountOfEntries<T>(this IEnumerable<Pair<T, T>> @this, T entry)
			=> @this.Count((p) => p.Item1.Equals(entry) || p.Item2.Equals(entry));
	}
}