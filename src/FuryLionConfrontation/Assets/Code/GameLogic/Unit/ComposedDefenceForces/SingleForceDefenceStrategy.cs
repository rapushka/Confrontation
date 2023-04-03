namespace Confrontation
{
	public class SingleForceDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;

		public SingleForceDefenceStrategy(IAssetsService assets, Garrison units)
			: base(assets)
			=> _units = units;

		public override float BaseDamage => _units.BaseDamage;

		public override int QuantityOfUnits => _units.QuantityOfUnits;

		public override void Destroy() => Assets.Destroy(_units.gameObject);

		public override void TakeDamageOnDefence(float damage) => _units.TakeDamageOnDefence(damage);
	}
}