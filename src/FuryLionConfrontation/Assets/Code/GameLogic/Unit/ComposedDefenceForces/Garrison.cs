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
		[SerializeField] private Coordinates _cellCoordinates;

		private int _quantityOfUnits;

		public SquadHealth Health { get; protected set; }

		public IUnitStats Stats { get; protected set; }

		public float AttackDamage => BaseStrength.IncreaseBy(Stats.AttackModifier);

		public float BaseStrength => Stats.BaseStrength * QuantityOfUnits;

		public float DefenceModifier => Stats.DefenseModifier;

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

		private int OwnerPlayerId => _field.Regions[Coordinates].OwnerPlayerId;

		public class Factory : PlaceholderFactory<Garrison>
		{
			[Inject] private readonly IAssetsService _assets;
			[Inject] private readonly IBalanceTable _balance;

			public Garrison Create(Cell cell, int quantityOfUnits = 0)
			{
				var garrison = base.Create();
				_assets.ToGroup(garrison.transform);

				garrison.transform.position = cell.Coordinates.ToAboveCellPosition();
				garrison.Coordinates = cell.Coordinates;
				garrison.QuantityOfUnits = quantityOfUnits;
				garrison.Stats = new UnitStatsDecorator(_balance.UnitStats, garrison.OwnerPlayerId, garrison.Field);
				garrison.Health = new SquadHealth(garrison);

				return garrison;
			}
		}
	}
}