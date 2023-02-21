namespace Confrontation
{
	public interface IField
	{
		CoordinatedMatrix<Cell> Cells { get; }

		CoordinatedMatrix<Building> Buildings { get; }

		CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }

		CoordinatedMatrix<Garrison> Garrisons { get; }

		CoordinatedMatrix<Region> Regions { get; }

		RegionsNeighbouring Neighbouring { get; }
	}
}