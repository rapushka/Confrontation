using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Region
	{
		[field: SerializeField] public Coordinates VillageCoordinates { get; private set; }

		[field: SerializeField] public List<Coordinates> CellsInRegion { get; private set; } = new();
	}
}