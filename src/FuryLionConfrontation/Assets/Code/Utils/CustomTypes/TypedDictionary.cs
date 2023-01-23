using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class TypedDictionary<T>
	{
		private readonly Dictionary<Type, T> _dictionary;

		public TypedDictionary(IEnumerable<T> collection) => _dictionary = collection.ToDictionary((w) => w.GetType());

		public T Get<TChild>()
			where TChild : T
			=> _dictionary[typeof(TChild)];
	}
}