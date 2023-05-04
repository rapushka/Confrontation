using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class TargetedInfluence
	{
		[field: SerializeField] public Influence       Influence { get; private set; }
		[field: SerializeField] public InfluenceTarget Target    { get; private set; }
	}
}