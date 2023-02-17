using Zenject;

namespace Confrontation
{
	public class UnitsDefenceStrategy : IDefenceStrategy
	{
		[Inject] private readonly IAssetsService _assets;

		private readonly Garrison _units;

		public UnitsDefenceStrategy(Garrison units) => _units = units;

		public int Quantity => _units.QuantityOfUnits;

		public void Destroy() => _assets.Destroy(_units.gameObject);

		public void TakeDamage(int damage) => _units.QuantityOfUnits -= damage;
	}
}