using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;
		[SerializeField] private QuantityOfUnitsInSquadView _quantityOfUnitsInSquadView;
		[SerializeField] private Cell _locationCell;

		private int _quantityOfUnits;
		[CanBeNull] private Cell _targetCell;

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.UpdateValue(value);
			}
		}

		public Player Owner { get; set; }

		public Cell LocationCell
		{
			get => _locationCell;
			set
			{
				_locationCell = value;
				MergeWithOtherSquad(value);
				_locationCell.UnitsSquads = this;
			}
		}

		private void MergeWithOtherSquad(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell))
			{
				var squadOnCell = cell.UnitsSquads;
				QuantityOfUnits += squadOnCell!.QuantityOfUnits;
				Destroy(squadOnCell.gameObject);
			}
		}

		private bool IsCellAlreadyPlaced(Cell value) => value.UnitsSquads == true && value.UnitsSquads != this;

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