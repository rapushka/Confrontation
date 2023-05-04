using Zenject;

namespace Confrontation
{
	public class OnOurFarmsInfluencer : OnOurBuildingsInfluencer<Farm>
	{
		public class Factory : PlaceholderFactory<IInfluencer, OnOurFarmsInfluencer> { }
	}
}