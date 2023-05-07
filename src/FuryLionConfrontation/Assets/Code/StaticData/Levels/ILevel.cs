using System.Collections.Generic;

namespace Confrontation
{
	public interface ILevel
	{
		Sizes                          Sizes         { get; }
		List<Region.Data>              Regions       { get; }
		List<Building.CoordinatedData> Buildings     { get; }
		List<TutorialPage>             TutorialPages { get; }
	}
}