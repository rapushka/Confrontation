using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Data Value { get; set; }

		[Serializable]
		public class Data
		{
			[field: SerializeField] public Coordinates       Sizes             { get; private set; }
			[field: SerializeField] public List<Coordinates> VillagesPositions { get; private set; }
		}
	}
}