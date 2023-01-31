using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class UnitOrderPerformer : MonoBehaviour
	{
		[SerializeField] private UnitsSquad _unitsSquad;

		private Cell _locationCell;
		[CanBeNull] private Cell _targetCell;

		public void PlaceInCell()
		{
			SetLocation(_targetCell);
			_targetCell = null;
		}

		public void SetLocation(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell) == false)
			{
				CaptureRegion(cell);
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
			=> cell.RelatedRegion.OwnerPlayerId == _unitsSquad.OwnerPlayerId;

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
				return;
			}

			if (_unitsSquad.QuantityOfUnits < enemySquad.QuantityOfUnits)
			{
				enemySquad.QuantityOfUnits -= _unitsSquad.QuantityOfUnits;
				Destroy(_unitsSquad.gameObject);
				return;
			}

			if (_unitsSquad.QuantityOfUnits == enemySquad.QuantityOfUnits)
			{
				cell.UnitsSquads = null;
				Destroy(_unitsSquad.gameObject);
				Destroy(enemySquad.gameObject);
				cell.MakeRegionNeutral();
			}
		}

		private void CaptureRegion(Cell cell)
		{
			AppropriateCell(cell);

			cell.RelatedRegion.SetOwner(_unitsSquad.OwnerPlayerId);
		}

		private void AppropriateCell(Cell cell)
		{
			_locationCell = cell;
			cell.UnitsSquads = _unitsSquad;
		}
	}
}