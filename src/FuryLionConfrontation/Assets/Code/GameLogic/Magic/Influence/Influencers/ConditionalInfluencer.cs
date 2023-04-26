using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public abstract class ConditionalInfluencer : InfluencerBase { }

	public class OnMovingUnitsInfluencer : ConditionalInfluencer
	{
		private HashSet<UnitsSquad> _influencedUnits;

		public bool HasInfluenced => _influencedUnits.Any();
		
		public float Influence(float on, InfluenceTarget withTarget, UnitsSquad @for)
		{
			if (_influencedUnits.Contains(@for) == false)
			{
				return on;
			}

			if (@for.IsMoving == false)
			{
				_influencedUnits.Remove(@for);
			}

			return Influence(on, withTarget);
		}

		public class Factory : PlaceholderFactory<OnMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnMovingUnitsInfluencer Create()
			{
				var influencer = base.Create();
				influencer._influencedUnits = _field.AllUnits.Except(_field.LocatedUnits).ToHashSet();
				return influencer;
			}
		}
	}
}