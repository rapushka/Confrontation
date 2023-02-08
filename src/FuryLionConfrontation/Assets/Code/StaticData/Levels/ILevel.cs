using System.Collections.Generic;

namespace Confrontation
{
	public interface ILevel
	{
		Sizes               Sizes        { get; }
		List<Region.Data>   Regions      { get; }
		List<Building.Data> Buildings    { get; }
	}
}