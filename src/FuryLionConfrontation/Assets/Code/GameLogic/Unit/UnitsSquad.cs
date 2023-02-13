using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;

		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		private int _quantityOfUnits;
		private Coordinates _coordinates;

		private void OnEnable() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDisable() => _unitMovement.TargetReached -= OnTargetCellReached;

		public int OwnerPlayerId { get; set; }

		public Cell LocationCell => _field.Cells[Coordinates];
		
		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.LocatedUnits.Add(this);
			}
		}

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.text = value.ToString();
			}
		}

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

		public class Factory : PlaceholderFactory<UnitsSquad>
		{
			public UnitsSquad Create(Vector3 position, Cell cell, int ownerPlayerId, int quantityOfUnits = 1)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = position;
				unitsSquad.OwnerPlayerId = ownerPlayerId;
				unitsSquad.Coordinates = cell.Coordinates;
				unitsSquad.Locate(cell);
				unitsSquad.QuantityOfUnits = quantityOfUnits;

				return unitsSquad;
			}
		}
	}
}