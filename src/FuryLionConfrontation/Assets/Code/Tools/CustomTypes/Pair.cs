using System;

namespace Confrontation
{
	public readonly struct Pair<T>
	{
		private readonly T _item1;
		private readonly T _item2;

		public Pair(T item1, T item2)
		{
			_item1 = item1;
			_item2 = item2;
		}

		public T Item1 => _item1;
		public T Item2 => _item2;

		public bool TryPickPairFor(T item, out T result)
		{
			if (item.Equals(Item1))
			{
				result = Item2;
				return true;
			}

			if (item.Equals(Item2))
			{
				result = Item1;
				return true;
			}

			result = default;
			return false;
		} 
		
		public override bool Equals(object obj) => obj is Pair<T> pair && Equals(pair);

		private bool Equals(Pair<T> pair) => pair.GetHashCode() == GetHashCode();

		public override int GetHashCode()
			=> _item1.GetHashCode() < _item2.GetHashCode()
				? HashCode.Combine(_item1, _item2)
				: HashCode.Combine(_item2, _item1);
	}
}