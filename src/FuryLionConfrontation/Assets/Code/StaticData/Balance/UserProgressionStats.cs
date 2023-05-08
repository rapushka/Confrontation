using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class UserProgressionStats
	{
		[field: SerializeField] public int KalymPerLevel { get; private set; }

		[field: SerializeField] public float LevelNumberMultiplier { get; private set; }
	}
}