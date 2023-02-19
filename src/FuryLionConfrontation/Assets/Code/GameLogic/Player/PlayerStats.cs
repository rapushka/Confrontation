using System;

namespace Confrontation
{
	public class PlayerStats
	{
		private int _goldCount;
		public event Action ValueChanged;

		public int GoldCount
		{
			get => _goldCount;
			set
			{
				ValueChanged?.Invoke();
				_goldCount = value;
			}
		}

		public bool IsEnoughGoldFor(int purchasePrice) => GoldCount >= purchasePrice;

		public void Spend(int gold)
		{
			GoldCount -= gold;
		}
	}
}