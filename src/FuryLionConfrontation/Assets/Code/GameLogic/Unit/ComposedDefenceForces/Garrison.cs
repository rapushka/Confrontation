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
		[SerializeField] private Coordinates _coordinates;

		private int _quantityOfUnits;

		public virtual Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Garrisons.Add(this);
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

		protected IField Field => _field;
		
		public class Factory : PlaceholderFactory<Garrison>
		{
			public Garrison Create(Cell cell, int quantityOfUnits = 1)
			{
				var unitsSquad = base.Create();
				unitsSquad.transform.position = cell.Coordinates.ToAboveCellPosition();
				unitsSquad.Coordinates = cell.Coordinates;
				unitsSquad.QuantityOfUnits = quantityOfUnits;

				return unitsSquad;
			}
		}
	}
}