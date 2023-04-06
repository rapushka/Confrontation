namespace Confrontation
{
	public class SingleForceDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;

		public SingleForceDefenceStrategy(IDestroyer destroyer, Garrison units)
			: base(destroyer)
			=> _units = units;

		public override float BaseDamage => _units.BaseDamage;

		public override int QuantityOfUnits => _units.QuantityOfUnits;

		public override void Destroy() => Destroyer.Destroy(_units.gameObject);
		public override void Kill()    => _units.QuantityOfUnits = 0;

		public override void TakeDamageOnDefence(float incomingDamage, float pierceRate)
			=> _units.Health.TakeDamageOnDefence(incomingDamage, pierceRate);
	}
}