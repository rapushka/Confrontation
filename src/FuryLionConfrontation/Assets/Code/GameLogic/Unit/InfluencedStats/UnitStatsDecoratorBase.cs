using Zenject;

namespace Confrontation
{
	public abstract class UnitStatsDecoratorBase : IUnitStats
	{
		[Inject] private readonly IUnitStats _decoratee;

		public virtual float BaseSpeed            => _decoratee.BaseSpeed;
		public virtual float BaseStrength         => _decoratee.BaseStrength;
		public virtual float BaseArmourMultiplier => _decoratee.BaseArmourMultiplier;
		public virtual float DefencePierceRate    => _decoratee.DefencePierceRate;
		public virtual float UnitMaxHp            => _decoratee.UnitMaxHp;
		public virtual float DefenseModifier      => _decoratee.DefenseModifier;
		public virtual float AttackModifier       => _decoratee.AttackModifier;
	}
}