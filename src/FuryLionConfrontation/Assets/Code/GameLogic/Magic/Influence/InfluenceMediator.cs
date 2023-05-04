using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;
		[Inject] private readonly PermanentInfluencer.Factory _permanentInfluencerFactory;
		[Inject] private readonly OnAllUntilMovingUnitsInfluencer.Factory _onAllUntilMovingUnitsFactory;
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onNowAllMovingUnitsFactory;
		[Inject] private readonly OnOurUnitsInfluencer.Factory _ouOurUnitsFactory;
		[Inject] private readonly OnOurForgesInfluencer.Factory _onOurForgesFactory;
		[Inject] private readonly OnOurFarmsInfluencer.Factory _onOurFarmsFactory;
		[Inject] private readonly OnOurGoldenMinesInfluencer.Factory _onOurGoldenMinesFactory;

		private readonly HashSet<IInfluencer> _influencers = new();

		public InfluenceStatus Status => InfluenceStatus.ForceLive;

		public void CastSpell(ISpell spell)
		{
			foreach (var influence in spell.Influences)
			{
				IInfluencer influencerBase = _influencerBaseFactory.Create(influence);

				influencerBase = spell.SpellType switch
				{
					SpellType.Temporary => _duratedInfluenceFactory.Create(spell.Duration, influencerBase),
					SpellType.Permanent => _permanentInfluencerFactory.Create(influencerBase),
					_                   => influencerBase,
				};

				influencerBase = AddConstraint(influence.InfluenceConstraint, influencerBase);

				_influencers.Add(influencerBase);
			}
		}

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influencers.Aggregate(on, (x, i) => i.Influence(x, withTarget));

		public float Influence<T>(float on, InfluenceTarget withTarget, T @for)
			=> _influencers.Aggregate(on, (x, i) => Influence(x, withTarget, @for, i));

		private static float Influence<T>(float current, InfluenceTarget withTarget, T @for, IInfluencer influencer)
			=> influencer is ConstrainedInfluencer<T> constrainedInfluencer
				? constrainedInfluencer.Influence(current, withTarget, @for)
				: influencer.Influence(current, withTarget);

		private IInfluencer AddConstraint(InfluenceConstraint constraint, IInfluencer influencer)
			=> constraint switch
			{
				InfluenceConstraint.AllUntilMovingUnits => _onAllUntilMovingUnitsFactory.Create(influencer),
				InfluenceConstraint.AllNowMovingUnits   => _onNowAllMovingUnitsFactory.Create(influencer),
				InfluenceConstraint.OurUnits            => _ouOurUnitsFactory.Create(influencer),
				InfluenceConstraint.OurFarms            => _onOurFarmsFactory.Create(influencer),
				InfluenceConstraint.OurForges           => _onOurForgesFactory.Create(influencer),
				InfluenceConstraint.OurGoldenMine       => _onOurGoldenMinesFactory.Create(influencer),
				_                                       => influencer,
			};

		private void ClearUnusedInfluencers()
			=> _influencers.RemoveWhere((i) => i.Status is InfluenceStatus.ForceDeath);
	}
}