using System.Collections.Generic;

namespace Confrontation
{
	public class ImmutableLevel : ILevel
	{
		public ImmutableLevel(Sizes sizes, List<Region.Data> regions, List<Building.CoordinatedData> buildings)
		{
			Sizes = sizes;
			Regions = regions;
			Buildings = buildings;
		}

		public Sizes                          Sizes     { get; }
		public List<Region.Data>              Regions   { get; }
		public List<Building.CoordinatedData> Buildings { get; }
	}
}