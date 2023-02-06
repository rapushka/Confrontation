using System.Collections.Generic;

namespace Confrontation
{
	public class Level
	{
		public int                PlayersCount { get; set; }
		public Sizes              Sizes        { get; set; }
		public List<RegionData>       Regions      { get; set; }
		public List<BuildingData> Buildings    { get; set; }
	}
}