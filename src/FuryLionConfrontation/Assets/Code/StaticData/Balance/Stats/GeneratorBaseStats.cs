using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public abstract class GeneratorBaseStats : IStats
	{
		[field: SerializeField] public float GenerationCoolDown { get; private set; }

		[field: SerializeField] public int GenerationAmount { get; private set; }
	}

	[Serializable] public class BarrackStats : GeneratorBaseStats { }
}