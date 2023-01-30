using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitOrderPerformer : MonoBehaviour
	{
		[SerializeField] private UnitsSquad _unitsSquad;

		private Cell _locationCell;
		[CanBeNull] private Cell _targetCell;

		public Cell LocationCell
		{
			set
			{
				_locationCell = value;
				MergeWithOtherSquad(value);
				_locationCell.UnitsSquads = _unitsSquad;
				CaptureRegion(_locationCell);
			}
		}

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_locationCell.UnitsSquads = null;

			if (quantityToMove < _unitsSquad.QuantityOfUnits
			    && quantityToMove > 0)
			{
				FormNewSquad(quantityToMove);
			}

			_locationCell = null;
			_targetCell = targetCell;
		}

		public void PlaceInCell()
		{
			LocationCell = _targetCell;
			_targetCell = null;
		}

		private void MergeWithOtherSquad(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell))
			{
				Merge(cell.UnitsSquads);
			}
		}

		private void CaptureRegion(Cell cell)
		{
			cell.RelatedRegion.OwnerPlayerId = _unitsSquad.OwnerPlayerId;
			foreach (var cellInRegion in cell.RelatedRegion.CellsInRegion)
			{
				cellInRegion.SetColor(_unitsSquad.OwnerPlayerId);
			}
		}

		private void FormNewSquad(int quantity)
		{
			var newSquad = Object.Instantiate(_unitsSquad);
			newSquad.OwnerPlayerId = _unitsSquad.OwnerPlayerId;
			newSquad.SetLocation(_locationCell);
			newSquad.QuantityOfUnits = quantity;
			_unitsSquad.QuantityOfUnits -= quantity;
		}

		private bool IsCellAlreadyPlaced(Cell value) => value.UnitsSquads == true && value.UnitsSquads != _unitsSquad;

		private void Merge(UnitsSquad squadOnCell)
		{
			_unitsSquad.QuantityOfUnits += squadOnCell!.QuantityOfUnits;
			Object.Destroy(squadOnCell.gameObject);
		}
	}
}