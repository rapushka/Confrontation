using Zenject;

namespace Confrontation
{
	public class BuildRandomBuildingOnRandomCellCommand : ICommand
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly Purchase _purchase;
		[Inject] private readonly Our _our;

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