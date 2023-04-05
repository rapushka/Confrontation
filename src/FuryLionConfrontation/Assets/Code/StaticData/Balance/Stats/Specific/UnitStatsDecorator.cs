using System.Linq;

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

		public float BaseStrength
		{
			get
			{
				var baseStrength = _decoratee.BaseStrength;

				var modifiedStrength = _field.Buildings
				                             .OfType<Forge>()
				                             .Where((f) => f.OwnerPlayerId == _ownerPlayer)
				                             .Aggregate(baseStrength, GetCombatStrengthFromForges);

				return modifiedStrength;
			}
		}

		public float DefenseModifier => _decoratee.DefenseModifier;

		public float AttackModifier => _decoratee.AttackModifier;

		public FloatToIntStrategy ConvertDamageToUnitsQuantity => _decoratee.ConvertDamageToUnitsQuantity;

		private static float GetCombatStrengthFromForges(float currentStrength, Forge forge)
			=> currentStrength + forge.CurrentLevelStats.CombatStrengthIncreasesRate;
	}
}