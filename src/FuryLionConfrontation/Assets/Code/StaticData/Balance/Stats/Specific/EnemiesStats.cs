using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class EnemiesStats : IStats
	{
		[field: SerializeField] public float SecondsBetweenActions { get; private set; }
	}
}