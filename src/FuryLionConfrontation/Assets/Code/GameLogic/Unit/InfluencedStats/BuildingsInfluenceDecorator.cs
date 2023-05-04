using System;
using Zenject;

namespace Confrontation
{
	public class BuildingsInfluenceDecorator : UnitStatsDecoratorBase
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly Garrison _garrison;

		public override float BaseStrength => Influence<Forge>(base.BaseStrength, AddStrength);

		public override float DefencePierceRate => Influence<Workshop>(base.DefencePierceRate, AddPierceRate);

		public override float BaseSpeed => Influence<Stable>(base.BaseSpeed, Accelerate);

		private static float AddStrength(float currentStrength, Forge forge)
			=> currentStrength + forge.CurrentLevelStats.CombatStrengthIncreasesRate;

		public override float DefenseModifier
		{
			get
			{
				var modified = Influence<Quarry>(base.DefenseModifier, AddDefenseModifier);
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
			=> _field.Buildings.InfluenceFloat(baseValue, _garrison.OwnerPlayerId, influence);

		private float InfluenceFort(float modified)
			=> _garrison is UnitsSquad squad && squad.LocationCell.Building is Fort fort
				? modified * fort.CurrentLevelStats.AdditionalDefenceModifier
				: modified;
	}
}