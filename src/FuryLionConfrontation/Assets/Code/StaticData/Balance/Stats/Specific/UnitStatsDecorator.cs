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

		public float BaseSpeed => _decoratee.BaseSpeed;

		public float UnitMaxHp => _decoratee.UnitMaxHp;

		public float BaseStrength
			=> _field.Buildings.InfluenceFloat<Forge>(_decoratee.BaseStrength, _ownerPlayer, AddStrength);

		public float DefenseModifier
			=> _field.Buildings.InfluenceFloat<Quarry>(_decoratee.DefenseModifier, _ownerPlayer, AddDefenseModifier);

		public float AttackModifier => _decoratee.AttackModifier;

		private static float AddStrength(float currentStrength, Forge forge)
			=> currentStrength + forge.CurrentLevelStats.CombatStrengthIncreasesRate;

		private static float AddDefenseModifier(float currentModifier, Quarry quarry)
			=> currentModifier + quarry.CurrentLevelStats.IncreasesDamageAbsorptionRate;
	}
}