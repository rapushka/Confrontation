namespace Confrontation
{
	public class Capital : Village
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;

		private Barracks _barracks;
		private GoldenMine _goldenMine;

		public void SetStashedBuildings(Barracks barracks, GoldenMine goldenMine)
		{
			barracks.Invisibility.MakeInvisible();
			goldenMine.Invisibility.MakeInvisible();

			_barracks = barracks;
			_goldenMine = goldenMine;
		}

		public override void Action()
		{
			base.Action();

			_barracks.Action();
			_goldenMine.Action();
		}
	}
}