using System;

namespace Confrontation
{
	public readonly struct Pair<T1, T2>
	{
		private readonly T1 _item1;
		private readonly T2 _item2;

		public Pair(T1 item1, T2 item2)
		{
			_item1 = item1;
			_item2 = item2;
		}

		public T1 Item1 => _item1;
		public T2 Item2 => _item2;

		public override bool Equals(object obj) => obj is Pair<T1, T2> pair && Equals(pair);

		private bool Equals(Pair<T1, T2> pair) => pair.GetHashCode() == GetHashCode();

		public override int GetHashCode()
			=> _item1.GetHashCode() < _item2.GetHashCode()
				? HashCode.Combine(_item1, _item2)
				: HashCode.Combine(_item2, _item1);
	}
}