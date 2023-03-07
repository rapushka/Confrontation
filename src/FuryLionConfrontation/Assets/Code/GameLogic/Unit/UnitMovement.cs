using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitMovement : MonoBehaviour
	{
		[Inject] private readonly ITimeService _timeService;
		[Inject] private readonly IBalanceTable _balance;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		[SerializeField] private Transform _transform;

		private Cell _targetCell;

		private float Speed => _balance.UnitStats.BaseSpeed;

		public event Action TargetReached;

		private float ScaledSpeed => Speed * _timeService.FixedDeltaTime;

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

		private async void MoveToTarget(CancellationTokenSource source)
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

		private bool IsTargetReach() => Vector3.Distance(CurrentPosition, TargetPosition) < Mathf.Epsilon;
	}
}