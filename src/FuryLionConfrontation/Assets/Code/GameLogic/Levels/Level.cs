using System.Collections.Generic;

namespace Confrontation
{
	public class Level : ILevel
	{
		public int                 PlayersCount { get; set; }
		public Sizes               Sizes        { get; set; }
		public List<Region.Data>   Regions      { get; set; }
		public List<Building.Data> Buildings    { get; set; }
	}
}