namespace Confrontation
{
	public class SingleForceDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;

		public SingleForceDefenceStrategy(IDestroyer destroyer, Garrison units)
			: base(destroyer)
			=> _units = units;

		public override float BaseDamage => _units.BaseStrength;

		public override int QuantityOfUnits { get => _units.QuantityOfUnits; set => _units.QuantityOfUnits = value; }

		public override void Destroy() => Destroyer.Destroy(_units.gameObject);

		public override void TakeDamageOnDefence(float incomingDamage) => _units.Health.TakeDamageOnDefence(incomingDamage);
	}
}