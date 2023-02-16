using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;

		[SerializeField] protected UnitAnimator _animator;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;

		private int _quantityOfUnits;

		public Coordinates Coordinates { get; set; }

		protected IField Field => _field;

		public int QuantityOfUnits
		{
			get => _quantityOfUnits;
			set
			{
				_quantityOfUnits = value;
				_quantityOfUnitsInSquadView.text = value.ToString();
			}
		}
		
		public class Factory : PlaceholderFactory<Garrison>
		{
			public Garrison Create(Cell cell, int quantityOfUnits = 1)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = InitialUnitPosition(cell.Coordinates);
				unitsSquad.Coordinates = cell.Coordinates;
				unitsSquad.QuantityOfUnits = quantityOfUnits;

				return unitsSquad;
			}
			
			private static Vector3 InitialUnitPosition(Coordinates coordinates)
				=> coordinates.CalculatePosition().AsTopDown() + Constants.VerticalOffsetAboveCell;
		}
	}
}