using System;

namespace Confrontation
{
	public class OnlyOneForceDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;

		public OnlyOneForceDefenceStrategy(IAssetsService assets, Garrison units)
			: base(assets)
			=> _units = units;

		public override int Quantity => _units.QuantityOfUnits;

		public override void Destroy() => Assets.Destroy(_units.gameObject);

		public override void TakeDamage(int damage)
		{
			_units.QuantityOfUnits -= damage;

			if (_units.QuantityOfUnits <= 0)
			{
				throw new InvalidOperationException("If forces quantity is 0 ─ then they lost");
			}
		}
	}
}