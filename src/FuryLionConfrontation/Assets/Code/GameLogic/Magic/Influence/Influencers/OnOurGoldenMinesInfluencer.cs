using Zenject;

namespace Confrontation
{
	public class OnOurGoldenMinesInfluencer : OnOurBuildingsInfluencer<GoldenMine>
	{
		public class Factory : PlaceholderFactory<IInfluencer, OnOurGoldenMinesInfluencer> { }
	}
}