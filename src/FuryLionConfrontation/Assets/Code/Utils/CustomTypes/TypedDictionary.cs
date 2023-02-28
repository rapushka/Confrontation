using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class TypedDictionary<T> : IEnumerable<T>
	{
		private readonly Dictionary<Type, T> _dictionary;

		public TypedDictionary(IEnumerable<T> collection) => _dictionary = collection.ToDictionary((w) => w.GetType());

		public TChild Get<TChild>()
			where TChild : T
			=> (TChild)_dictionary[typeof(TChild)];

		public IEnumerable<T> Where(Func<T, bool> predicate)
		{
			foreach (var (_, value) in _dictionary)
			{
				if (predicate(value))
				{
					yield return value;
				}
			}
		}

		public IEnumerator<T> GetEnumerator() => _dictionary.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _dictionary.Values.GetEnumerator();
	}
}