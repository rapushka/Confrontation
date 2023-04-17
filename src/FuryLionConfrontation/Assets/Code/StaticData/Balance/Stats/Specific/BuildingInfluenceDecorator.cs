using System;

namespace Confrontation
{
	public class BuildingInfluenceDecorator : IUnitStats
	{
		private readonly IUnitStats _decoratee;
		private readonly int _ownerPlayer;
		private readonly IField _field;
		private readonly Garrison _garrison;

		public BuildingInfluenceDecorator(IUnitStats decoratee, int ownerPlayer, IField field, Garrison garrison)
		{
			_decoratee = decoratee;
			_ownerPlayer = ownerPlayer;
			_field = field;
			_garrison = garrison;
		}

		public float AttackModifier => _decoratee.AttackModifier;

		public float UnitMaxHp => _decoratee.UnitMaxHp;

		public float BaseArmourMultiplier => _decoratee.BaseArmourMultiplier;

		public float BaseStrength => Influence<Forge>(_decoratee.BaseStrength, AddStrength);

		public float DefencePierceRate => Influence<Workshop>(_decoratee.DefencePierceRate, AddPierceRate);

		public float BaseSpeed => Influence<Stable>(_decoratee.BaseSpeed, Accelerate);

		private static float AddStrength(float currentStrength, Forge forge)
			=> currentStrength + forge.CurrentLevelStats.CombatStrengthIncreasesRate;

		public float DefenseModifier
		{
			get
			{
				var modified = Influence<Quarry>(_decoratee.DefenseModifier, AddDefenseModifier);
				modified = InfluenceFort(modified);
				return modified;
			}
		}

		private static float AddDefenseModifier(float currentModifier, Quarry quarry)
			=> currentModifier + quarry.CurrentLevelStats.IncreasesDamageAbsorptionRate;

		private static float AddPierceRate(float currentPierceRate, Workshop workshop)
			=> currentPierceRate + workshop.CurrentLevelStats.DecreaseEnemyDamageAbsorptionRate;

		private static float Accelerate(float currentSpeed, Stable stable)
			=> currentSpeed + stable.CurrentLevelStats.UnitsAccelerationCoefficient;

		private float Influence<TBuilding>(float baseValue, Func<float, TBuilding, float> influence)
			where TBuilding : Building
			=> _field.Buildings.InfluenceFloat(baseValue, _ownerPlayer, influence);

		private float InfluenceFort(float modified)
			=> _garrison is UnitsSquad squad && squad.LocationCell.Building is Fort fort
				? modified * fort.CurrentLevelStats.AdditionalDefenceModifier
				: modified;
	}
}