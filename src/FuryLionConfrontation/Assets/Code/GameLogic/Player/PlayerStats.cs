using System;
using UnityEngine.Assertions;

namespace Confrontation
{
	public class PlayerStats
	{
		public event Action ValueChanged;

		public int GoldCount { get; private set; }

		public bool IsEnoughGoldFor(int purchasePrice) => GoldCount >= purchasePrice;

		public void Earn(int gold)
		{
			Assert.IsTrue(gold > 0);

			GoldCount += gold;
			ValueChanged?.Invoke();
		}

		public void Spend(int gold)
		{
			Assert.IsTrue(gold > 0);
			Assert.IsTrue(GoldCount >= gold);

			GoldCount -= gold;
			ValueChanged?.Invoke();
		}
	}
}