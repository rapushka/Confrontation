namespace Confrontation
{
	public interface IField
	{
		CoordinatedMatrix<Cell>       Cells        { get; }
		CoordinatedMatrix<Building>   Buildings    { get; }
		CoordinatedMatrix<UnitsSquad> LocatedUnits { get; }
	}
}