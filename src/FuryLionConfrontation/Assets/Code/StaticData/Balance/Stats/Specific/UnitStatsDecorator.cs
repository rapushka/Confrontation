using System;

namespace Confrontation
{
	public class UnitStatsDecorator : IUnitStats
	{
		private readonly IUnitStats _decoratee;
		private readonly int _ownerPlayer;
		private readonly IField _field;

		public UnitStatsDecorator(IUnitStats decoratee, int ownerPlayer, IField field)
		{
			_decoratee = decoratee;
			_ownerPlayer = ownerPlayer;
			_field = field;
		}

		public float AttackModifier => _decoratee.AttackModifier;

		public float UnitMaxHp => _decoratee.UnitMaxHp;

		public float BaseArmourMultiplier => _decoratee.BaseArmourMultiplier;

		public float BaseStrength => Influence<Forge>(_decoratee.BaseStrength, AddStrength);

		public float DefenseModifier => Influence<Quarry>(_decoratee.DefenseModifier, AddDefenseModifier);

		public float DefencePierceRate => Influence<Workshop>(_decoratee.DefencePierceRate, AddPierceRate);

		public float BaseSpeed => Influence<Stable>(_decoratee.BaseSpeed, Accelerate);

		private static float AddStrength(float currentStrength, Forge forge)
			=> currentStrength + forge.CurrentLevelStats.CombatStrengthIncreasesRate;

		private static float AddDefenseModifier(float currentModifier, Quarry quarry)
			=> currentModifier + quarry.CurrentLevelStats.IncreasesDamageAbsorptionRate;

		private static float AddPierceRate(float currentPierceRate, Workshop workshop)
			=> currentPierceRate + workshop.CurrentLevelStats.DecreaseEnemyDamageAbsorptionRate;

		private static float Accelerate(float currentSpeed, Stable stable)
			=> currentSpeed + stable.CurrentLevelStats.UnitsAccelerationCoefficient;

		private float Influence<TBuilding>(float baseValue, Func<float, TBuilding, float> influence)
			where TBuilding : Building
			=> _field.Buildings.InfluenceFloat(baseValue, _ownerPlayer, influence);
	}
}