using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private TextMeshPro _quantityOfUnitsInSquadView;

		private Coordinates _cellCoordinates;
		private int _quantityOfUnits;

		public SquadHealth Health { get; private set; }

		public IUnitStats Stats { get; private set; }

		public float HealthPoints => Health.HealthPoints;

		public float AttackDamage => BaseDamage.IncreaseBy(Stats.AttackModifier);

		public float BaseArmor => BaseStrength * Stats.BaseArmourMultiplier;

		public float BaseDamage => BaseStrength;

		public float DefenceModifier => Stats.DefenseModifier;

		public float DefencePierceRate => Stats.DefencePierceRate;

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

		private float BaseStrength => Stats.BaseStrength * QuantityOfUnits;

		private int OwnerPlayerId => _field.Regions[Coordinates].OwnerPlayerId;

		public void Kill() => QuantityOfUnits = 0;

		protected void Initialize(Coordinates coordinates, int quantityOfUnits, IUnitStats baseStats)
		{
			transform.position = coordinates.ToAboveCellPosition();
			Coordinates = coordinates;
			QuantityOfUnits = quantityOfUnits;
			Stats = new BuildingInfluenceDecorator(baseStats, OwnerPlayerId, Field, this);
			Health = new SquadHealth(this);
		}

		public class Factory : PlaceholderFactory<Garrison>
		{
			[Inject] private readonly IAssetsService _assets;
			[Inject] private readonly IStatsTable _stats;

			public Garrison Create(Cell cell, int quantityOfUnits = 0)
			{
				var garrison = base.Create();
				_assets.ToGroup(garrison.transform);
				garrison.Initialize(cell.Coordinates, quantityOfUnits, _stats.UnitStats);

				return garrison;
			}
		}
	}
}