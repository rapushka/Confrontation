using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Influence
	{
		[field: SerializeField] public float           Modifier    { get; private set; }
		[field: SerializeField] public InfluenceTarget Target      { get; private set; }
		[field: SerializeField] public InfluenceConstraint     InfluenceConstraint { get; private set; }

		public Influence(Influence influence)
		{
			Modifier = influence.Modifier;
			Target = influence.Target;
		}

		public float Apply(float baseValue) => baseValue * Modifier;
	}
}