using System;
using System.Collections.Generic;

namespace Confrontation
{
	[Serializable]
	public class LeveledList<T> : List<T>
	{
		public LeveledList(IEnumerable<T> list) : base(list) { }

		public new T this[int level] => base[level - 1];

		public int MaxLevel => Count;
	}
}