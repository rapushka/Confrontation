using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class LeveledStats<T> : IStats
		where T : IStats
	{
		[SerializeField] private List<T> _statsByLevel = new();

		public int MaxLevel => _statsByLevel.Count;

		public T this[int level]
		{
			get => _statsByLevel[level - 1];
			set => _statsByLevel[level - 1] = value;
		}
	}
}