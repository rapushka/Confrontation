using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitsSquad : Garrison
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		private void OnEnable() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDisable() => _unitMovement.TargetReached -= OnTargetCellReached;

		public int OwnerPlayerId { get; set; }

		public Cell LocationCell => Field.Cells[Coordinates];

		private void Locate(Cell cell) => _unitOrderPerformer.Locate(cell);

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_unitOrderPerformer.MoveTo(targetCell, quantityToMove);
			_unitMovement.MoveTo(targetCell);
			_animator.StartMoving();
		}

		private void OnTargetCellReached()
		{
			_unitOrderPerformer.PlaceInCell();
			_animator.StopMoving();
		}

		public new class Factory : PlaceholderFactory<UnitsSquad>
		{
			public UnitsSquad Create(Cell cell, int ownerPlayerId, int quantityOfUnits = 1)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = InitialUnitPosition(cell.Coordinates);
				unitsSquad.OwnerPlayerId = ownerPlayerId;
				unitsSquad.Coordinates = cell.Coordinates;
				unitsSquad.Locate(cell);
				unitsSquad.QuantityOfUnits = quantityOfUnits;

				return unitsSquad;
			}

			private static Vector3 InitialUnitPosition(Coordinates coordinates)
				=> coordinates.CalculatePosition().AsTopDown() + Constants.VerticalOffsetAboveCell;
		}
	}
}