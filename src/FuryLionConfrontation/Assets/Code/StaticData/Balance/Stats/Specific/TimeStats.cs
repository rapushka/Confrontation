using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class TimeStats
	{
		[field: SerializeField] public float NormalTimeScale { get; private set; }

		[field: SerializeField] public float AcceleratedTimeScale { get; private set; }
	}
}