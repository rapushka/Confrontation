using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class BarrackData
	{
		[field: SerializeField] public float CoolDownDuration { get; private set; }

		[field: SerializeField] public int SpawnAmount { get; private set; }
	}
}