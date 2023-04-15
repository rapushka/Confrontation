using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class TargetedInfluence
	{
		[field: SerializeField] public Influence Influence          { get; private set; }
		[field: SerializeField] public Target    TargetForInfluence { get; private set; }

		public enum Target
		{
			UnitsSpeed,
		}
	}
}