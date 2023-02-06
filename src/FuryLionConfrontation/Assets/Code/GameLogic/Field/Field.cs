using Zenject;

namespace Confrontation
{
	public class Field : IField
	{
		[Inject]
		public Field(ILevelSelector levelSelector)
		{
			Cells = new CoordinatedMatrix<Cell>(levelSelector.SelectedLevel.Sizes);
			Buildings = new CoordinatedMatrix<Building>(levelSelector.SelectedLevel.Sizes);
			LocatedUnits = new CoordinatedMatrix<UnitsSquad>(levelSelector.SelectedLevel.Sizes);
			Regions = new CoordinatedMatrix<Region>(levelSelector.SelectedLevel.Sizes);
		}

		public CoordinatedMatrix<Cell>     Cells     { get; }
		public CoordinatedMatrix<Building> Buildings { get; }
		public CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }
		public CoordinatedMatrix<Region> Regions { get; }
	}
}