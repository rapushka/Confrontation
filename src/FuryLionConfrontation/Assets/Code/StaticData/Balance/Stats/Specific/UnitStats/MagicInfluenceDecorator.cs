namespace Confrontation
{
	public class MagicInfluenceDecorator : IUnitStats
	{
		private readonly IUnitStats _decoratee;
		private Garrison _garrison;

		public MagicInfluenceDecorator(IUnitStats decoratee, Garrison garrison)
		{
			_decoratee = decoratee;
			_garrison = garrison;
		}

		public float BaseSpeed => _decoratee.BaseSpeed;

		public float BaseStrength => _decoratee.BaseStrength;

		public float BaseArmourMultiplier => _decoratee.BaseArmourMultiplier;

		public float DefencePierceRate => _decoratee.DefencePierceRate;

		public float UnitMaxHp => _decoratee.UnitMaxHp;

		public float DefenseModifier => _decoratee.DefenseModifier;

		public float AttackModifier => _decoratee.AttackModifier;
	}
}