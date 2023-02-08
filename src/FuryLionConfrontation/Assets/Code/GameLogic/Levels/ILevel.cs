using System.Collections.Generic;

namespace Confrontation
{
	public interface ILevel
	{
		int                 PlayersCount { get; set; }
		Sizes               Sizes        { get; set; }
		List<Region.Data>   Regions      { get; set; }
		List<Building.Data> Buildings    { get; set; }
	}
}