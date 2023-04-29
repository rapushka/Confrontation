using System.Collections.Generic;

namespace Confrontation
{
	public class ConfigurableField : IField
	{
		private Sizes _sizes;

		public ConfigurableField(ILevelSelector levelSelector)
		{
			_sizes = levelSelector.SelectedLevel.Sizes;
			Recreate();
		}

		public CoordinatedMatrix<Cell>       Cells            { get; private set; }
		public CoordinatedMatrix<Building>   Buildings        { get; private set; }
		public List<Building>                StashedBuildings { get; private set; }
		public CoordinatedMatrix<UnitsSquad> LocatedUnits     { get; private set; }
		public List<UnitsSquad>              AllUnits         { get; private set; }
		public CoordinatedMatrix<Garrison>   Garrisons        { get; private set; }
		public CoordinatedMatrix<Region>     Regions          { get; private set; }
		public List<Player>                  Players          { get; private set; }

		public Sizes Sizes
		{
			get => _sizes;
			set
			{
				_sizes = value;
				Recreate();
			}
		}

		public RegionsNeighborhoodContainer Neighborhoods { get; private set; }

		private void Recreate()
		{
			Cells = new CoordinatedMatrix<Cell>(_sizes);
			Buildings = new CoordinatedMatrix<Building>(_sizes);
			StashedBuildings = new List<Building>();
			LocatedUnits = new CoordinatedMatrix<UnitsSquad>(_sizes);
			AllUnits = new List<UnitsSquad>();
			Garrisons = new CoordinatedMatrix<Garrison>(_sizes);
			Regions = new CoordinatedMatrix<Region>(_sizes);
			Players = new List<Player>();
			Neighborhoods = new RegionsNeighborhoodContainer();
		}
	}
}