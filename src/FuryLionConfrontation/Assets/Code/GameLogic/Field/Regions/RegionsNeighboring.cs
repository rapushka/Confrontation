using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class RegionsNeighboring
	{
		public RegionsNeighboring() => Neighborhoods = new HashSet<Pair<int, int>>();

		public HashSet<Pair<int, int>> Neighborhoods { get; }

		public bool IsNeighbours(Region first, Region second)
			=> Neighborhoods.Contains(new Pair<int, int>(first.Id, second.Id));

		public void AddNeighboring(Region first, Region second)
			=> Neighborhoods.Add(new Pair<int, int>(first.Id, second.Id));
	}

	public static class PairedHashSetExtensions
	{
		public static int CountOfEntries<T>(this IEnumerable<Pair<T, T>> @this, T entry)
			=> @this.Count((p) => p.Item1.Equals(entry) || p.Item2.Equals(entry));
	}
}