using System;
using UnityEngine.Assertions;

namespace Confrontation
{
	public class Resource
	{
		private int _count;

		public event Action ValueChanged;

		public bool IsEnoughGoldFor(int desiredCount) => _count >= desiredCount;

		public void Earn(int value)
		{
			Assert.IsTrue(value > 0);

			_count += value;
			ValueChanged?.Invoke();
		}

		public void Spend(int value)
		{
			Assert.IsTrue(value > 0);
			Assert.IsTrue(_count >= value);

			_count -= value;
			ValueChanged?.Invoke();
		}
	}
}