using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class TypedDictionary<T>
	{
		private readonly Dictionary<Type, T> _dictionary;

		public TypedDictionary() => _dictionary = new Dictionary<Type, T>();

		public TypedDictionary(IEnumerable<T> collection) => _dictionary = collection.ToDictionary((w) => w.GetType());

		public TChild Get<TChild>()
			where TChild : T
			=> (TChild)_dictionary[typeof(TChild)];

		public void Add<TChild>(TChild value)
			where TChild : T
			=> _dictionary.Add(typeof(TChild), value);

		public bool ContainsKey<TChild>() where TChild : T => _dictionary.ContainsKey(typeof(TChild));
	}
}