using Zenject;

namespace Confrontation
{
	public class SingleForceDefenceStrategy : IDefenceStrategy
	{
		[Inject] private readonly Garrison _units;
		[Inject] private readonly IDestroyService _destroyService;

		public float BaseDamage => _units.BaseDamage;

		public int QuantityOfUnits => _units.QuantityOfUnits;

		public float HealthPoints => _units.HealthPoints;

		public void Destroy() => _destroyService.Destroy(_units.gameObject);

		public void Kill() => _units.QuantityOfUnits = 0;

		public void TakeDamageOnDefence(float incomingDamage, float pierceRate)
			=> _units.Health.TakeDamageOnDefence(incomingDamage, pierceRate);

		public class Factory : PlaceholderFactory<Garrison, SingleForceDefenceStrategy> { }
	}
}