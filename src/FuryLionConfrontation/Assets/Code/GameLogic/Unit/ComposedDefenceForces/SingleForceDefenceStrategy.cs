using UnityEngine.Assertions;

namespace Confrontation
{
	public class SingleForceDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;

		public SingleForceDefenceStrategy(IAssetsService assets, Garrison units)
			: base(assets)
			=> _units = units;

		public override int DefenceStrength => _units.DefenceStrength;

		public override void Destroy() => Assets.Destroy(_units.gameObject);

		public override void TakeDamage(int damage)
		{
			_units.QuantityOfUnits -= damage;

			Assert.IsTrue(_units.QuantityOfUnits > 0, message: WrongStrategyException);
		}
	}
}