using Zenject;

namespace Confrontation
{
	public class InfluencerBase : IInfluencer
	{
		[Inject] private Influence _influence;

		public bool IsAlive => false;

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influence.Target == withTarget ? _influence.Apply(on) : on;

		public class Factory : PlaceholderFactory<Influence, InfluencerBase> { }
	}
}