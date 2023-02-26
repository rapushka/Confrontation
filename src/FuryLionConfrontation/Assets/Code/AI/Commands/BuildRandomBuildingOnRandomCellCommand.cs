namespace Confrontation
{
	public class BuildRandomBuildingOnRandomCellCommand : ICommand
	{
		private readonly Player _player;
		private readonly Purchase _purchase;
		private readonly Our _our;

		public BuildRandomBuildingOnRandomCellCommand(Our our,  Player player, Purchase purchase)
		{
			_our = our;
			_player = player;
			_purchase = purchase;
		}

		public void Execute()
		{
			if (_our.CanBeBoughtBuildings.TryPickRandom(out var building)
			    && _our.EmptyCells.TryPickRandom(out var emptyCell))
			{
				_purchase.BuyBuilding(_player, building, emptyCell);
			}
		}
	}
}