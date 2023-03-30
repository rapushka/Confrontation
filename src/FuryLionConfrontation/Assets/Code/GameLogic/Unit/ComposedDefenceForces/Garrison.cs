using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IBalanceTable _balance;

		private const int FullStrength = 1;

		[SerializeField] protected UnitAnimator _animator;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;
		[SerializeField] private Coordinates _cellCoordinates;

		private int _quantityOfUnits;

		public int AttackStrength => Mathf.RoundToInt(QuantityOfUnits * AttackStrengthOfSingleUnit);

		public int DefencedQuantity => Mathf.RoundToInt(QuantityOfUnits * DefenceStrengthOfSingleUnit);

		public float BaseStrength => Stats.BaseStrength * QuantityOfUnits;

		public float AttackStrengthOfSingleUnit => BaseStrength * (Stats.AttackModifier + FullStrength);

		public float DefenceStrengthOfSingleUnit => Stats.DefenseModifier + FullStrength;

		private UnitStats Stats => _balance.UnitStats;

		public virtual Coordinates Coordinates
		{
			get => _cellCoordinates;
			set
			{
				_cellCoordinates = value;
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
			[Inject] private readonly IAssetsService _assets;

			public Garrison Create(Cell cell, int quantityOfUnits = 0)
			{
				var garrison = base.Create();
				_assets.ToGroup(garrison.transform);
				garrison.transform.position = cell.Coordinates.ToAboveCellPosition();
				garrison.Coordinates = cell.Coordinates;
				garrison.QuantityOfUnits = quantityOfUnits;

				return garrison;
			}
		}
	}
}