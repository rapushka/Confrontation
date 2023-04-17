using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitsSquad : Garrison
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		private Coordinates _coordinates;

		private void OnEnable() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDisable() => _unitMovement.TargetReached -= OnTargetCellReached;

		public int OwnerPlayerId { get; set; }

		public Cell LocationCell => Field.Cells[Coordinates];

		public override Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				Field.LocatedUnits.Add(this);
			}
		}

		public void MoveTo(Cell targetCell)
		{
			var quantityToMove = LocationCell.Building is Barrack or Fort
				? QuantityOfUnits
				: QuantityOfUnits / 2;

			MoveTo(targetCell, quantityToMove);
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
			[Inject] private readonly IAssetsService _assets;
			[Inject] private readonly IStatsTable _stats;

			public UnitsSquad Create(Cell cell, int quantityOfUnits = 0)
			{
				var squad = base.Create();
				_assets.ToGroup(squad.transform);
				squad.OwnerPlayerId = cell.OwnerPlayerId;
				squad.Initialize(cell, quantityOfUnits, _stats.UnitStats);

				return squad;
			}
		}
	}
}