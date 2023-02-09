namespace Confrontation
{
	public class Capital : Village
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;
	}
}