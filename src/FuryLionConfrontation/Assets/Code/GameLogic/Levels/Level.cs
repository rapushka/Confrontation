using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Sizes Sizes { get; set; }

		[field: SerializeField] public List<RegionData> Regions { get; private set; } = new();
	}

	[Serializable]
	public class RegionData
	{
		[field: SerializeField] public Coordinates VillageCoordinates { get; private set; }

		[field: SerializeField] public List<Coordinates> CellsInRegion { get; private set; }
	}
}