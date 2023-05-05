using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class GeneratorStatsBase : IGeneratorStats
	{
		[field: SerializeField] public virtual float CoolDown { get; private set; }

		[field: SerializeField] public virtual int Amount { get; private set; }
	}
}