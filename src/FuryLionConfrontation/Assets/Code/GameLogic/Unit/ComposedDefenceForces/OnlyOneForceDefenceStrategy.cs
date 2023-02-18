using UnityEngine.Assertions;

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

			Assert.IsTrue(_units.QuantityOfUnits > 0, message: WrongStrategyException);
		}
	}
}