using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitsSquad : Garrison
	{
		[Inject] private readonly IField _field;

		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		private void OnEnable() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDisable() => _unitMovement.TargetReached -= OnTargetCellReached;

		public int OwnerPlayerId { get; set; }

		public Cell LocationCell => _field.Cells[Coordinates];

		public override Coordinates Coordinates
		{
			get => base.Coordinates;
			set
			{
				base.Coordinates = value;
				_field.LocatedUnits.Add(this);
			}
		}

		public void MoveTo(Cell targetCell, int quantityToMove)
		{
			_unitOrderPerformer.MoveTo(targetCell, quantityToMove);
			_unitMovement.MoveTo(targetCell);
			_animator.StartMoving();
		}

		private void OnTargetCellReached()
		{
			_unitOrderPerformer.LocateInTargetCell();
			_animator.StopMoving();
		}

		public new class Factory : PlaceholderFactory<UnitsSquad>
		{
			public UnitsSquad Create(Cell cell, int ownerPlayerId, int quantityOfUnits = 1)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = cell.Coordinates.ToAboveCellPosition();
				unitsSquad.OwnerPlayerId = ownerPlayerId;
				unitsSquad.Coordinates = cell.Coordinates;
				unitsSquad.QuantityOfUnits = quantityOfUnits;

				return unitsSquad;
			}
		}
	}
}