using System.Collections.Generic;

namespace Confrontation
{
	public class RegionsNeighborhoodContainer
	{
		public HashSet<Pair<int, int>> Neighborhoods { get; } = new();

		public bool IsNeighbours(Region first, Region second)
			=> Neighborhoods.Contains(new Pair<int, int>(first.Id, second.Id));

		public void Add(Region first, Region second)
			=> Neighborhoods.Add(new Pair<int, int>(first.Id, second.Id));
	}
}