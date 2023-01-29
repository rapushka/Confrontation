using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		private const float MinDistance = 0.01f;
		private static readonly Vector3 VerticalOffsetAboveCell = Vector3.up * 0.2f;

		[SerializeField] private Transform _transform;
		[SerializeField] private float _speed;

		public             Player Owner           { get; set; }
		public             Cell   LocationCell    { get; set; }
		[CanBeNull] public Cell   TargetCell      { get; set; }
		public             int    QuantityOfUnits { get; set; }

		private float ScaledSpeed => _speed * Time.fixedDeltaTime;

		private Vector3 TargetPosition => TargetCell!.transform.position + VerticalOffsetAboveCell;

		private Vector3 CurrentPosition => _transform.position;

		private void FixedUpdate()
		{
			if (TargetCell is not null)
			{
				MoveToTarget();
			}
		}

		private void MoveToTarget()
		{
			_transform.position = Vector3.MoveTowards(current: CurrentPosition, target: TargetPosition, ScaledSpeed);

			if (IsReachTarget())
			{
				LocationCell = TargetCell;
				TargetCell = null;
			}
		}

		private bool IsReachTarget() => Vector3.Distance(CurrentPosition, TargetPosition) < MinDistance;
	}
}