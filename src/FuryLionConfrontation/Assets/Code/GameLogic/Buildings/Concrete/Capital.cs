namespace Confrontation
{
	public class Capital : Village
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;

		public Barracks Barracks;
		public GoldenMine GoldenMine;

		public override void Action()
		{
			base.Action();

			Barracks.Action();
			GoldenMine.Action();
		}
	}
}