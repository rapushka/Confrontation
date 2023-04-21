using System.Linq;
using UnityEngine;
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
				var baseSpeed = _decoratee.BaseSpeed;
				var speed = baseSpeed;

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
			    && _influences.WithTarget(InfluenceTarget.MovingUnitsSpeed).Any())
			{
				Debug.Log("slowed");
				_isSlowed = true;
			}
		}

		private float Influence(float speed)
			=> _influences.Influence(speed, InfluenceTarget.MovingUnitsSpeed);

		private void Reset() => _isSlowed = false;
	}
}