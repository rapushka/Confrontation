using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class BalanceEntry : IBalanceEntry
	{
		[SerializeField] private GoldenMine _goldenMine;

		public IBalanceEntry.IGoldenMine GoldenMineEntry => _goldenMine;

		[Serializable]
		public class GoldenMine : IBalanceEntry.IGoldenMine
		{
			[field: SerializeField] public int ProduceSpeed  { get; private set; }
			[field: SerializeField] public int ProduceAmount { get; private set; }
		}
	}
}