using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Influence
	{
		[SerializeField] private Type _type;

		[field: SerializeField] public float Coefficient { get; private set; }

		public float Apply(float baseValue)
			=> _type switch
			{
				Type.Modifier => baseValue * Coefficient,
				Type.Additive => baseValue + Coefficient,
				var _         => throw new ArgumentOutOfRangeException(),
			};

		private enum Type
		{
			Modifier,
			Additive,
		}
	}
}