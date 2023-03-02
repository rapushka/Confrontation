using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

		private TimeSpan FixedDeltaTimeSpan => _timeService.FixedDeltaTime.FromSeconds();

		public void MoveTo(Cell target)
		{
			_targetCell = target;

			LookAtTarget();
			MoveToTarget(this.GetCancellationTokenOnDestroy());
		}

		private void LookAtTarget()
		{
			if ((TargetPosition - CurrentPosition).normalized != Vector3.zero)
			{
				transform.forward = (TargetPosition - CurrentPosition).normalized;
			}
		}

		private async void MoveToTarget(CancellationToken token = default)
		{
			while (IsTargetReach() == false)
			{
				_transform.position = MoveTowardsTarget();

				if (await SuppressCancellationThrow(token))
				{
					return;
				}
			}

			TargetReached?.Invoke();
		}

		private async Task<bool> SuppressCancellationThrow(CancellationToken token)
			=> await UniTask.Delay(FixedDeltaTimeSpan, cancellationToken: token)
			                .SuppressCancellationThrow();

		private Vector3 MoveTowardsTarget() => Vector3.MoveTowards(CurrentPosition, TargetPosition, ScaledSpeed);

		private bool IsTargetReach() => Vector3.Distance(CurrentPosition, TargetPosition) < Mathf.Epsilon;
	}

	public static class TimeSpanExtensions
	{
		public static TimeSpan FromSeconds(this float @this)
		{
			return TimeSpan.FromSeconds(@this);
		}
	}
}