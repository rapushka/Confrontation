using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Region
	{
		[field: SerializeField] public int OwnerPlayerId { get; private set; }

		[field: SerializeField] public Coordinates VillageCoordinates { get; private set; }

		[field: SerializeField] public List<Coordinates> CellsCoordinates { get; private set; } = new();
	}
}