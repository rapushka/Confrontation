using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class BlizzardInfluenceDecorator : UnitStatsDecoratorBase
	{
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly InfluenceMediator _influenceMediator;

		public override float BaseSpeed
		{
			get
			{
				var speed = base.BaseSpeed;

				speed = _influenceMediator.Influence(on: speed, withTarget: AllMovingUnitsSpeed, @for: _squad);

				return speed;
			}
		}
	}
}