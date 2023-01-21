using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Sizes Sizes { get; private set; }

		[field: SerializeField] public List<Village.Data> Regions { get; private set; } = new();

		public ILookup<Coordinates, Coordinates> RegionsAsLookup()
			=> Regions
			   .SelectMany((r) => r.Cells, (r, c) => (VillageCoordinates: r.Coordinates, Cell: c))
			   .ToLookup((x) => x.VillageCoordinates, (x) => x.Cell);
	}
}