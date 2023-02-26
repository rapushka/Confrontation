using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Entry<T>
	{
		[field: Range(0, 1)] [field: SerializeField] public float Weight { get; private set; }

		[field: SerializeField] public T Item { get; private set; }
	}
}