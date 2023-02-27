using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitOrderPerformer : MonoBehaviour
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly IAssetsService _assets;

		[SerializeField] public UnitsSquad _unitsSquad;

		private UnitFighter _unitFighter;
		[CanBeNull] private Cell _targetCell;

		private void OnEnable() => _unitFighter = new UnitFighter(_unitsSquad, _assets);

		public void LocateInTargetCell()
		{
			Locate(_targetCell);
			_targetCell = null;
		}

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_unitsSquad.LocationCell.DetachUnitsSquad();

			if (quantityToMove < _unitsSquad.QuantityOfUnits
			    && quantityToMove > 0)
			{
				FormNewSquad(quantityToMove);
			}

			_targetCell = targetCell;
		}

		private void Locate(Cell cell)
		{
			if (IsAlreadyPlaced(cell) == false)
			{
				_unitFighter.CaptureRegion(cell);
				return;
			}

			if (IsHaveSameOwner(cell))
			{
				MergeWith(cell.LocatedUnits);
				return;
			}

			_unitFighter.FightWithSquadOn(cell);
		}

		private void FormNewSquad(int quantity)
		{
			_unitsFactory.Create(_unitsSquad.LocationCell, quantity);
			_unitsSquad.QuantityOfUnits -= quantity;
		}

		private bool IsAlreadyPlaced(Cell cell)
			=> (cell.LocatedUnits == true
			    && cell.LocatedUnits != _unitsSquad)
			   || (cell.Garrison == true
			       && cell.RelatedRegion!.OwnerPlayerId != _unitsSquad.OwnerPlayerId);

		private bool IsHaveSameOwner(Cell cell) => cell.OwnerPlayerId == _unitsSquad.OwnerPlayerId;

		private void MergeWith(Garrison units)
		{
			units.QuantityOfUnits += _unitsSquad.QuantityOfUnits;
			_assets.Destroy(_unitsSquad.gameObject);
		}
	}
}