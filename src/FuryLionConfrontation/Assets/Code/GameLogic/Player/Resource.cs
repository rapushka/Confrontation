using System;
using UnityEngine.Assertions;

namespace Confrontation
{
	public class Resource
	{
		public event Action ValueChanged;

		public int Count { get; private set; }

		public bool IsEnoughGoldFor(int desiredCount) => Count >= desiredCount;

		public void Earn(int value)
		{
			Assert.IsTrue(value > 0);

			Count += value;
			ValueChanged?.Invoke();
		}

		public void Spend(int value)
		{
			Assert.IsTrue(value > 0);
			Assert.IsTrue(Count >= value);

			Count -= value;
			ValueChanged?.Invoke();
		}
	}
}