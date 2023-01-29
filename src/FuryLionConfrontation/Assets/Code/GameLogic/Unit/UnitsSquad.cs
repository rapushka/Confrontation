using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;

		[CanBeNull] private Cell _targetCell;

		private void Start()
		{
			_unitMovement.TargetReached += OnTargetCellReached;
		}

		public Player Owner { get; set; }

		public Cell LocationCell { get; set; }

		public int QuantityOfUnits { get; set; }

		[CanBeNull]
		public Cell TargetCell
		{
			get => _targetCell;
			set
			{
				_unitMovement.MoveTo(value);
				_animator.StartMoving();
				_targetCell = value;
			}
		}

		private void OnTargetCellReached()
		{
			LocationCell = _targetCell;
			_targetCell = null;
			_animator.StopMoving();
		}
	}
}