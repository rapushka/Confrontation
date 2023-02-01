using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class UnitsSquad : MonoBehaviour
	{
		[SerializeField] private UnitMovement _unitMovement;
		[SerializeField] private UnitAnimator _animator;
		[SerializeField] private QuantityOfUnitsInSquadView _quantityOfUnitsInSquadView;
		[SerializeField] private UnitOrderPerformer _unitOrderPerformer;

		private int _quantityOfUnits;

		private void OnEnable() => _unitMovement.TargetReached += OnTargetCellReached;

		private void OnDisable() => _unitMovement.TargetReached -= OnTargetCellReached;

		public int OwnerPlayerId { get; set; }

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.UpdateValue(value);
			}
		}

		public void SetLocation(Cell cell) => _unitOrderPerformer.SetLocation(cell);

		public void MoveTo(Cell targetCell) => MoveTo(targetCell, QuantityOfUnits);

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
			public UnitsSquad Create(Vector3 position, Cell cell)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = position;
				unitsSquad.OwnerPlayerId = cell.RelatedRegion.OwnerPlayerId;
				unitsSquad.SetLocation(cell);
				unitsSquad.QuantityOfUnits = 1;

				return unitsSquad;
			}
		}
	}
}