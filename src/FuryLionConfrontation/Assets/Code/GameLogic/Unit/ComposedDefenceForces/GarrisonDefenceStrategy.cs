using System;

namespace Confrontation
{
	public class GarrisonDefenceStrategy : IDefenceStrategy
	{
		private readonly Garrison _garrison;

		public GarrisonDefenceStrategy(Garrison garrison) => _garrison = garrison;

		public int Quantity => _garrison.QuantityOfUnits;

		public void Destroy()
		{
			throw new NotImplementedException();
		}

		public void TakeDamage(int damage)
		{
			throw new NotImplementedException();
		}
	}
}