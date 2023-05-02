using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class BlizzardInfluenceDecorator : UnitStatsDecoratorBase
	{
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly InfluenceMediator _influenceMediator;

		public override float BaseSpeed
			=> _influenceMediator.Influence(on: base.BaseSpeed, withTarget: AllUntillMovingUnitsSpeed, @for: _squad);

		public override float BaseStrength
			=> _influenceMediator.Influence(on: base.BaseStrength, withTarget: AllNowMovingUnitsStrength, @for: _squad);
	}
}