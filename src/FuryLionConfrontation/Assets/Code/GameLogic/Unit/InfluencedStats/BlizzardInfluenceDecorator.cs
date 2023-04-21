using Zenject;

namespace Confrontation
{
	public class BlizzardInfluenceDecorator : UnitStatsDecoratorBase, ILateTickable
	{
		[Inject] private readonly IUnitStats _decoratee;
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly ActiveInfluences _influences;

		private bool _isSlowed;

		private bool IsTargetReached => _squad.IsMoving == false;

		public override float BaseSpeed
		{
			get
			{
				var speed = _decoratee.BaseSpeed;

				CheckForCastedSpell();
				speed = Influence(speed);

				return speed;
			}
		}

		public void LateTick()
		{
			if (IsTargetReached)
			{
				Reset();
			}
		}

		private void CheckForCastedSpell()
		{
			if (_isSlowed == false
			    && _squad.IsMoving
			    && _influences.WithTarget(InfluenceTarget.MovingUnitsSpeed).AnyNegativeInfluence())
			{
				_isSlowed = true;
			}
		}

		private float Influence(float speed)
			=> _isSlowed ? _influences.Influence(speed, InfluenceTarget.MovingUnitsSpeed) : speed;

		private void Reset() => _isSlowed = false;
	}
}