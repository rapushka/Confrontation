using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class Field : IField
	{
		[Inject]
		public Field(ILevelSelector levelSelector)
		{
			var selectedLevelSizes = levelSelector.SelectedLevel.Sizes;

			Cells = new CoordinatedMatrix<Cell>(selectedLevelSizes);
			Buildings = new CoordinatedMatrix<Building>(selectedLevelSizes);
			StashedBuildings = new List<Building>();
			LocatedUnits = new CoordinatedMatrix<UnitsSquad>(selectedLevelSizes);
			Regions = new CoordinatedMatrix<Region>(selectedLevelSizes);
			Garrisons = new CoordinatedMatrix<Garrison>(selectedLevelSizes);
			Neighborhoods = new RegionsNeighborhoodContainer();
		}

		public CoordinatedMatrix<Cell> Cells { get; }

		public CoordinatedMatrix<Building> Buildings { get; }

		public List<Building> StashedBuildings { get; }

		public CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }

		public CoordinatedMatrix<Garrison> Garrisons { get; }

		public CoordinatedMatrix<Region> Regions { get; }

		public RegionsNeighborhoodContainer Neighborhoods { get; }

		public List<Player> Players { get; } = new();
	}
}