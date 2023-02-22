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
			LocatedUnits = new CoordinatedMatrix<UnitsSquad>(selectedLevelSizes);
			Regions = new CoordinatedMatrix<Region>(selectedLevelSizes);
			Garrisons = new CoordinatedMatrix<Garrison>(selectedLevelSizes);
			Neighboring = new RegionsNeighboring();
		}

		public CoordinatedMatrix<Cell> Cells { get; }

		public CoordinatedMatrix<Building> Buildings { get; }

		public CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }

		public CoordinatedMatrix<Garrison> Garrisons { get; }

		public CoordinatedMatrix<Region> Regions { get; }

		public RegionsNeighboring Neighboring { get; }

		public List<Player> Players { get; } = new();
	}
}