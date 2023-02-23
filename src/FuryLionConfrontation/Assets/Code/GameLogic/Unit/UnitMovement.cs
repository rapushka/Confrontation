using System;
using System.Collections;
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
		private readonly WaitForFixedUpdate _waitForFixedUpdate = new();

		private float Speed => _balance.UnitStats.BaseSpeed;

		public event Action TargetReached;

		private float ScaledSpeed => Speed * _timeService.FixedDeltaTime;

		private Vector3 TargetPosition => _targetCell.transform.position + Constants.VerticalOffsetAboveCell;

		private Vector3 CurrentPosition => _transform.position;

		public void MoveTo(Cell target)
		{
			_targetCell = target;

			LookAtTarget();
			_routinesRunner.StartUnstoppableRoutine(MoveToTarget());
		}

		private void LookAtTarget()
		{
			if ((TargetPosition - CurrentPosition).normalized != Vector3.zero)
			{
				transform.forward = (TargetPosition - CurrentPosition).normalized;
			}
		}

		private IEnumerator MoveToTarget()
		{
			while (IsTargetReach() == false)
			{
				_transform.position = MoveTowardsTarget();
				yield return _waitForFixedUpdate;
			}

			TargetReached?.Invoke();
		}

		private Vector3 MoveTowardsTarget() => Vector3.MoveTowards(CurrentPosition, TargetPosition, ScaledSpeed);

		private bool IsTargetReach() => Vector3.Distance(CurrentPosition, TargetPosition) < Mathf.Epsilon;
	}
}