using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public abstract class ConditionalInfluencer : InfluencerBase { }

	public class OnMovingUnitsInfluencer : ConditionalInfluencer
	{
		private IEnumerable<UnitsSquad> _influencedUnits;

		public override float Influence(float on, InfluenceTarget withTarget)
		{
			return base.Influence(on, withTarget);
		}

		public class Factory : PlaceholderFactory<OnMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnMovingUnitsInfluencer Create()
			{
				var influencer = base.Create();
				influencer._influencedUnits = _field.AllUnits.Except(_field.LocatedUnits);
				return influencer;
			}
		}
	}
}