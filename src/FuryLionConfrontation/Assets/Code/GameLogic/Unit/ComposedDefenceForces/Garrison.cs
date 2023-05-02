using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Garrison : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly TextMeshPro _quantityOfUnitsInSquadView;
		[Inject] private readonly SquadHealth _health;
		[Inject] private readonly IUnitStats _stats;

		private Coordinates _cellCoordinates;
		private int _quantityOfUnits;

		public IUnitStats Stats => _stats;

		public SquadHealth Health => _health;

		public virtual int OwnerPlayerId
		{
			get => _field.Cells[Coordinates].OwnerPlayerId;
			set => _field.Cells[Coordinates].OwnerPlayerId = value;
		}

		public float HealthPoints => _health.HealthPoints;

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

		public void Kill() => QuantityOfUnits = 0;

		protected void SetUp(Coordinates coordinates, int quantityOfUnits)
		{
			transform.position = coordinates.ToAboveCellPosition();
			Coordinates = coordinates;
			QuantityOfUnits = quantityOfUnits;
		}

		public class Factory : PlaceholderFactory<Garrison>
		{
			[Inject] private readonly IAssetsService _assets;

			public Garrison Create(Cell cell, int quantityOfUnits = 0)
			{
				var garrison = base.Create();
				_assets.ToGroup(garrison.transform);
				garrison.SetUp(cell.Coordinates, quantityOfUnits);

				return garrison;
			}
		}
	}
}