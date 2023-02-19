using System.Collections.Generic;

namespace Confrontation
{
	public class LeveledStats<T> : IStats
		where T : IStats
	{
		private readonly List<T> _statsByLevel = new();

		public int MaxLevel => _statsByLevel.Count;

		public T this[int level]
		{
			get => _statsByLevel[level - 1];
			set => _statsByLevel[level - 1] = value;
		}
	}
}