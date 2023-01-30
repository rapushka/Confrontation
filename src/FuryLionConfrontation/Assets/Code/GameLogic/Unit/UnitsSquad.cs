using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;
		[SerializeField] private QuantityOfUnitsInSquadView _quantityOfUnitsInSquadView;

		private Cell _locationCell;
		private int _quantityOfUnits;
		[CanBeNull] private Cell _targetCell;

		public Player Owner { get; set; }

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.UpdateValue(value);
			}
		}

		public Cell LocationCell
		{
			set
			{
				_locationCell = value;
				MergeWithOtherSquad(value);
				_locationCell.UnitsSquads = this;
			}
		}

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_locationCell.UnitsSquads = null;

			if (quantityToMove < QuantityOfUnits
			    && quantityToMove > 0)
			{
				FormNewSquad(quantityToMove);
			}

			_locationCell = null;
			_targetCell = targetCell;
			_unitMovement.MoveTo(_targetCell);
			_animator.StartMoving();
		}

		private void FormNewSquad(int quantity)
		{
			var newSquad = Instantiate(this);
			newSquad.LocationCell = _locationCell;
			newSquad.QuantityOfUnits = quantity;
			QuantityOfUnits -= quantity;
		}

		private void MergeWithOtherSquad(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell))
			{
				Merge(cell.UnitsSquads);
			}
		}

		private void Merge(UnitsSquad squadOnCell)
		{
			QuantityOfUnits += squadOnCell!.QuantityOfUnits;
			Destroy(squadOnCell.gameObject);
		}

		private bool IsCellAlreadyPlaced(Cell value) => value.UnitsSquads == true && value.UnitsSquads != this;

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