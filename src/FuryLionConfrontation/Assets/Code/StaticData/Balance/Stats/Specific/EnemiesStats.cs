using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class EnemiesStats : IStats
	{
		[field: SerializeField] public Range SecondsBetweenActions { get; private set; }
	}
}