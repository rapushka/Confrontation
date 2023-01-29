using System;
using System.Collections;
using UnityEngine;

namespace Confrontation
{
	public class UnitMovement : MonoBehaviour
	{
		[SerializeField] private Transform _transform;
		[SerializeField] private float _speed = 1f;

		private Cell _targetCell;
		private readonly WaitForFixedUpdate _waitForFixedUpdate = new();
		private Vector3 _initialTransformForward;

		public event Action TargetReached;

		private float ScaledSpeed => _speed * Time.fixedDeltaTime;

		private Vector3 TargetPosition => _targetCell.transform.position + Constants.VerticalOffsetAboveCell;

		private Vector3 CurrentPosition => _transform.position;

		public void MoveTo(Cell target)
		{
			_targetCell = target;

			_initialTransformForward = transform.forward;
			transform.forward = (TargetPosition - CurrentPosition).normalized;
			StartCoroutine(MoveToTarget());
		}

		private IEnumerator MoveToTarget()
		{
			while (IsReachTarget() == false)
			{
				_transform.position = MoveTowardsTarget();
				yield return _waitForFixedUpdate;
			}

			TargetReached?.Invoke();
		}

		private Vector3 MoveTowardsTarget() => Vector3.MoveTowards(CurrentPosition, TargetPosition, ScaledSpeed);

		private bool IsReachTarget() => Vector3.Distance(CurrentPosition, TargetPosition) < Constants.Epsilon;
	}
}