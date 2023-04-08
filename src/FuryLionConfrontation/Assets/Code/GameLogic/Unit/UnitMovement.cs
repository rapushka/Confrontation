using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitMovement : MonoBehaviour
	{
		[Inject] private readonly ITimeService _timeService;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		[SerializeField] private Transform _transform;
		[SerializeField] private UnitsSquad _squad;

		private Cell _targetCell;

		public event Action TargetReached;

		private float Speed => Stats.BaseSpeed;

		private IUnitStats Stats => _squad.Stats;

		private float ScaledSpeed => Speed * _timeService.FixedDeltaTime;

		private float DistanceToTarget => Vector3.Distance(CurrentPosition, TargetPosition);

		private Vector3 TargetPosition => _targetCell.transform.position + Constants.VerticalOffsetAboveCell;

		private Vector3 CurrentPosition => _transform.position;

		public void MoveTo(Cell target)
		{
			_targetCell = target;

			LookAtTarget();
			_routinesRunner.StartRoutine(MoveToTarget);
		}

		private void LookAtTarget()
		{
			if ((TargetPosition - CurrentPosition).normalized != Vector3.zero)
			{
				transform.forward = (TargetPosition - CurrentPosition).normalized;
			}
		}

		private async Task MoveToTarget(CancellationTokenSource source)
		{
			while (source.Token.IsCancellationRequested == false
			       && IsTargetReach() == false)
			{
				_transform.position = MoveTowardsTarget();

				if (await source.Token.WaitForFixedUpdate())
				{
					return;
				}
			}

			TargetReached?.Invoke();
		}

		private Vector3 MoveTowardsTarget() => Vector3.MoveTowards(CurrentPosition, TargetPosition, ScaledSpeed);

		private bool IsTargetReach() => DistanceToTarget < Constants.Deviation;
	}
}