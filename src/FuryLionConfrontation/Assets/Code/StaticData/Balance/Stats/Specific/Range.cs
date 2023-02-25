using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Range
	{
		[field: SerializeField] public float Min { get; private set; }
		[field: SerializeField] public float Max { get; private set; }

		public float RandomNumberInRange => UnityEngine.Random.Range(Min, Max);
	}
}