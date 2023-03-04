namespace Confrontation
{
	public class Capital : Settlement
	{
		public int OwnerPlayerId => Field.Regions[Coordinates].OwnerPlayerId;

		private Barrack _barrack;
		private GoldenMine _goldenMine;

		public void SetStashedBuildings(Barrack barrack, GoldenMine goldenMine)
		{
			barrack.Invisibility.MakeInvisible();
			goldenMine.Invisibility.MakeInvisible();

			_barrack = barrack;
			_goldenMine = goldenMine;
		}

		public override void Action()
		{
			base.Action();
			_barrack.Action();
			_goldenMine.Action();
		}

		public override void LevelUp()
		{
			base.LevelUp();
			_barrack.LevelUp();
			_goldenMine.LevelUp();
		}
	}
}