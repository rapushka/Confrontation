using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class WeightedCollection<T>
	{
		[SerializeField] private List<Entry<T>> _entries = new();

		public T PickRandom()
		{
			var totalWeight = _entries.Sum((x) => x.Weight);
			var randomRate = UnityEngine.Random.value * totalWeight;
			var currentWeight = 0f;

			foreach (var entry in _entries)
			{
				currentWeight += entry.Weight;
				if (currentWeight >= randomRate)
				{
					return entry.Item;
				}
			}

			throw new Exception("Weight out of range");
		}
	}
}