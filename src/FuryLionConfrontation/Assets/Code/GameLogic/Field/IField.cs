using System.Collections.Generic;

namespace Confrontation
{
	public interface IField
	{
		CoordinatedMatrix<Cell> Cells { get; }

		CoordinatedMatrix<Building> Buildings { get; }

		List<Building> StashedBuildings { get; }

		CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }

		List<UnitsSquad> AllUnits { get; }

		CoordinatedMatrix<Garrison> Garrisons { get; }

		CoordinatedMatrix<Region> Regions { get; }

		RegionsNeighborhoodContainer Neighborhoods { get; }
	}
}