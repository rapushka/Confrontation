using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Confrontation
{
	[Serializable]
	public class BalanceEntry : IBalanceEntry
	{
		[FormerlySerializedAs("_goldenMine")] [SerializeField] private GoldenMineData _goldenMineData;

		public IBalanceEntry.IGoldenMine GoldenMineEntry => _goldenMineData;

		[Serializable]
		public class GoldenMineData : IBalanceEntry.IGoldenMine
		{
			[field: SerializeField] public float ProduceCollDownDuration { get; private set; }

			[field: SerializeField] public int ProduceAmount { get; private set; }
		}
	}
}