using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;

		[CanBeNull] private Cell _targetCell;
		[SerializeField] private Cell _locationCell;

		[field: SerializeField] public int QuantityOfUnits { get; set; }

		public Player Owner { get; set; }

		public Cell LocationCell
		{
			get => _locationCell;
			set
			{
				_locationCell = value;
				if (value.UnitsSquads == true
				    && value.UnitsSquads != this)
				{
					var squadOnCell = value.UnitsSquads;
					QuantityOfUnits += squadOnCell!.QuantityOfUnits;
					Destroy(squadOnCell.gameObject);
				}

				_locationCell.UnitsSquads = this;
			}
		}

		[CanBeNull]
		public Cell TargetCell
		{
			get => _targetCell;
			set
			{
				_locationCell.UnitsSquads = null;
				_unitMovement.MoveTo(value);
				_animator.StartMoving();
				_targetCell = value;
			}
		}

		private void Start() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDestroy() => _unitMovement.TargetReached -= OnTargetCellReached;

		private void OnTargetCellReached()
		{
			LocationCell = _targetCell;
			_targetCell = null;
			_animator.StopMoving();
		}
	}
}