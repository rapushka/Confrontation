using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class GoldenMineData : IGoldenMine
	{
		[field: SerializeField] public float ProduceCollDownDuration { get; private set; }

		[field: SerializeField] public int ProduceAmount { get; private set; }
	}
}