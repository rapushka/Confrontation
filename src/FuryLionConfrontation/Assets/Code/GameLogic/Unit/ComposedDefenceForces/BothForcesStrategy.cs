using System;
using Zenject;

namespace Confrontation
{
	public class BothForcesStrategy : IDefenceStrategy
	{
		[Inject] private readonly IAssetsService _assets;

		private readonly Garrison _locatedUints;
		private readonly Garrison _garrison;
		private readonly IFlexDefenceStrategy _flexDefence;

		public BothForcesStrategy(IFlexDefenceStrategy flexDefence, Garrison locatedUints, Garrison garrison)
		{
			_flexDefence = flexDefence;
			_locatedUints = locatedUints;
			_garrison = garrison;
		}

		public int Quantity => _locatedUints.QuantityOfUnits + _garrison.QuantityOfUnits;

		public void Destroy()
		{
			_assets.Destroy(_locatedUints.gameObject);
			_assets.Destroy(_garrison.gameObject);
		}

		public void TakeDamage(int damage)
		{
			throw new NotImplementedException();
		}
	}
}