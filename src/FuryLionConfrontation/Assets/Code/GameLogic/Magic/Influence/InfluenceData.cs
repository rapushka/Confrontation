using System;
using UnityEngine;

namespace Confrontation.Influence
{
	[Serializable]
	public class InfluenceData
	{
		[SerializeField] private Type _type;

		[field: SerializeField] public float Coefficient { get; private set; }

		public float Influence(float baseValue)
			=> _type switch
			{
				Type.Modifier   => baseValue.IncreaseBy(Coefficient),
				Type.Multiplier => baseValue * Coefficient,
				Type.Additive   => baseValue + Coefficient,
				var _           => throw new ArgumentOutOfRangeException(),
			};

		private enum Type
		{
			Modifier,
			Multiplier,
			Additive,
		}
	}
}