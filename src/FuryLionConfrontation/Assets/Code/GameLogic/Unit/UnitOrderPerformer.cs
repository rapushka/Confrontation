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
			set => DoActionWithOtherSquad(value);
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

		private void DoActionWithOtherSquad(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell) == false)
			{
				CaptureRegion(_locationCell);
				return;
			}

			if (IsHaveSameOwner(cell))
			{
				MergeWith(cell.UnitsSquads);
			}
			else
			{
				FightWithSquadOn(cell);
			}
		}

		private void FormNewSquad(int quantity)
		{
			var newSquad = Instantiate(_unitsSquad);
			newSquad.OwnerPlayerId = _unitsSquad.OwnerPlayerId;
			newSquad.SetLocation(_locationCell);
			newSquad.QuantityOfUnits = quantity;
			_unitsSquad.QuantityOfUnits -= quantity;
		}

		private bool IsCellAlreadyPlaced(Cell value) => value.UnitsSquads == true && value.UnitsSquads != _unitsSquad;

		private bool IsHaveSameOwner(Cell cell)
			=> cell.RelatedRegion.OwnerPlayerId == _locationCell.RelatedRegion.OwnerPlayerId;

		private void MergeWith(UnitsSquad squadOnCell)
		{
			squadOnCell.QuantityOfUnits += _unitsSquad.QuantityOfUnits;
			Destroy(_unitsSquad.gameObject);
		}

		private void FightWithSquadOn(Cell cell)
		{
			var enemySquad = cell.UnitsSquads!;

			if (_unitsSquad.QuantityOfUnits > enemySquad.QuantityOfUnits)
			{
				_unitsSquad.QuantityOfUnits -= enemySquad.QuantityOfUnits;
				Destroy(enemySquad.gameObject);
				CaptureRegion(cell);
			}
			else if (_unitsSquad.QuantityOfUnits < enemySquad.QuantityOfUnits)
			{
				enemySquad.QuantityOfUnits -= _unitsSquad.QuantityOfUnits;
				Destroy(_unitsSquad.gameObject);
			}
			else
			{
				cell.UnitsSquads = null;
				Destroy(_unitsSquad.gameObject);
				Destroy(enemySquad.gameObject);
				cell.MakeRegionNeutral();
			}
		}

		private void AppropriateCell(Cell cell)
		{
			_locationCell = cell;
			cell.UnitsSquads = _unitsSquad;
		}

		private void CaptureRegion(Cell cell)
		{
			AppropriateCell(cell);

			cell.RelatedRegion.SetOwner(_unitsSquad.OwnerPlayerId);
		}
	}
}