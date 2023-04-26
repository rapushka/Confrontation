using System.Linq;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class BlizzardInfluenceDecorator : UnitStatsDecoratorBase, ILateTickable
	{
		[Inject] private readonly IUnitStats _decoratee;
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly InfluencesWithDuration _influencesWithDuration;

		private bool _isSlowed;
		private float _cachedSlowRate;

		private bool IsTargetReached => _squad.IsMoving == false;

		public override float BaseSpeed
		{
			get
			{
				var baseSpeed = _decoratee.BaseSpeed;
				var speed = baseSpeed;

				CheckForCastedSpell(baseSpeed);
				speed -= _cachedSlowRate;

				FinishSlowing();

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

		private void CheckForCastedSpell(float baseSpeed)
		{
			if (_isSlowed == false
			    && _squad.IsMoving
			    && _influencesWithDuration.WithTarget(MovingUnitsSpeed).Any())
			{
				_isSlowed = true;
				_cachedSlowRate = baseSpeed - Influence(baseSpeed);
			}
		}

		private float Influence(float speed)
			=> _influencesWithDuration.Influence(on: speed, withTarget: MovingUnitsSpeed);

		private void FinishSlowing()
		{
			if (_isSlowed
			    && _squad.IsMoving == false)
			{
				_isSlowed = false;
				_cachedSlowRate = 0;
			}
		}

		private void Reset() => _isSlowed = false;
	}
}