using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class LeveledStats<T> : IStats
		where T : IStats
	{
		[field: SerializeField] public int UpgradePrice { get; private set; }

		[field: SerializeField] private List<T> StatsByLevel { get; set; } = new();

		public int MaxLevel => StatsByLevel.Count;

		public T this[int level]
		{
			get => StatsByLevel[level - 1];
			set => StatsByLevel[level - 1] = value;
		}
	}
}