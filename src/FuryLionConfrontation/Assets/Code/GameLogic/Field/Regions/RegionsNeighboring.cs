using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class RegionsNeighboring
	{
		public Dictionary<Region, IEnumerable<Region>> Neighbouring;

		public bool IsNeighbours(Region first, Region second) => Neighbouring[first].Contains(second);
	}
}