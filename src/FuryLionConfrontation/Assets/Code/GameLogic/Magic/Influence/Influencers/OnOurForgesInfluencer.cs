using Zenject;

namespace Confrontation
{
	public class OnOurForgesInfluencer : OnOurBuildingsInfluencer<Forge>
	{
		public class Factory : PlaceholderFactory<IInfluencer, OnOurForgesInfluencer> { }
	}
}