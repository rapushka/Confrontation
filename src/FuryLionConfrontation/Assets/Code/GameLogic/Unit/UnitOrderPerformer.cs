using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitOrderPerformer : MonoBehaviour
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IField _field;

		[SerializeField] private UnitsSquad _unitsSquad;

		[CanBeNull] private Cell _targetCell;

		public void PlaceInCell()
		{
			Locate(_targetCell);
			_targetCell = null;
		}

		public void Locate(Cell cell)
		{
			if (IsCellAlreadyPlaced(cell) == false)
			{
				CaptureRegion(cell);
				return;
			}

			if (IsHaveSameOwner(cell))
			{
				MergeWith(cell.LocatedUnits);
			}
			else
			{
				FightWithSquadOn(cell);
			}
		}

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_field.LocatedUnits.Remove(_unitsSquad);

			if (quantityToMove < _unitsSquad.QuantityOfUnits
			    && quantityToMove > 0)
			{
				FormNewSquad(quantityToMove);
			}

			_targetCell = targetCell;
		}

		private void FormNewSquad(int quantity)
		{
			_unitsFactory.Create(transform.position, _unitsSquad.LocationCell, _unitsSquad.OwnerPlayerId, quantity);
			_unitsSquad.QuantityOfUnits -= quantity;
		}

		private bool IsCellAlreadyPlaced(Cell cell) => cell.LocatedUnits == true && cell.LocatedUnits != _unitsSquad;

		private bool IsHaveSameOwner(Cell cell) => cell.OwnerPlayerId == _unitsSquad.OwnerPlayerId;

		private void MergeWith(UnitsSquad squadOnCell)
		{
			squadOnCell.QuantityOfUnits += _unitsSquad.QuantityOfUnits;
			_assets.Destroy(_unitsSquad.gameObject);
		}

		private void FightWithSquadOn(Cell cell)
		{
			var enemySquad = cell.LocatedUnits!;

			if (IsOurVictory(cell, enemySquad))
			{
				return;
			}

			if (IsEnemyVictory(enemySquad))
			{
				return;
			}

			IsDraw(cell, enemySquad);
		}

		private bool IsOurVictory(Cell cell, UnitsSquad enemySquad)
		{
			if (_unitsSquad.QuantityOfUnits > enemySquad.QuantityOfUnits)
			{
				_unitsSquad.QuantityOfUnits -= enemySquad.QuantityOfUnits;
				_assets.Destroy(enemySquad.gameObject);
				CaptureRegion(cell);
				return true;
			}

			return false;
		}

		private bool IsEnemyVictory(UnitsSquad enemySquad)
		{
			if (_unitsSquad.QuantityOfUnits < enemySquad.QuantityOfUnits)
			{
				enemySquad.QuantityOfUnits -= _unitsSquad.QuantityOfUnits;
				_assets.Destroy(_unitsSquad.gameObject);
				return true;
			}

			return false;
		}

		private void IsDraw(Cell cell, UnitsSquad enemySquad)
		{
			if (_unitsSquad.QuantityOfUnits == enemySquad.QuantityOfUnits)
			{
				_field.LocatedUnits.Remove(_unitsSquad);
				_assets.Destroy(_unitsSquad.gameObject);
				_assets.Destroy(enemySquad.gameObject);
				cell.MakeRegionNeutral();
			}
		}

		private void CaptureRegion(Cell cell)
		{
			_unitsSquad.Coordinates = cell.Coordinates;
			cell.RelatedRegion!.OwnerPlayerId = _unitsSquad.OwnerPlayerId;
		}
	}
}