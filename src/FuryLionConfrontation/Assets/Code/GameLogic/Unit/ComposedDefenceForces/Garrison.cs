using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IBalanceTable _balance;

		[SerializeField] protected UnitAnimator _animator;
		[SerializeField] private TextMeshPro _quantityOfUnitsInSquadView;
		[SerializeField] private Coordinates _cellCoordinates;

		private int _quantityOfUnits;

		public float AttackDamage => BaseDamage.IncreaseBy(Stats.AttackModifier);

		public float BaseDamage => Stats.BaseStrength * QuantityOfUnits;

		public float DefenceModifier => Stats.DefenseModifier;

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

		public float TakeDamageOnDefence(float incomeDamage)
			=> TakeDamage(incomeDamage.ReduceBy(DefenceModifier));

		public float TakeDamage(float incomingDamage)
		{
			var remainedUnits = QuantityOfUnits - Mathf.FloorToInt(incomingDamage);
			var isSquadSurvived = remainedUnits > 0;
			var initialQuantity = QuantityOfUnits;

			QuantityOfUnits = isSquadSurvived ? remainedUnits : 0;
			var overkillDamage = isSquadSurvived ? 0 : incomingDamage - initialQuantity;
			return overkillDamage;
		}

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