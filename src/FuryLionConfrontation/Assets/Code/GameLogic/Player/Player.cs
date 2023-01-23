using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Player : ScriptableObject
	{
		[field: SerializeField] public int Id { get; private set; }
	}
}