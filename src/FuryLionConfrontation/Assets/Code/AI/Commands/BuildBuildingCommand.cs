using Zenject;

namespace Confrontation
{
	public class BuildBuildingCommand : ICommand
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly Cell _cell;
		[Inject] private readonly Player _player;
		[Inject] private readonly Purchase _purchase;

		public void Execute() => _purchase.BuyBuilding(_player, _building, _cell);

		public class Factory : PlaceholderFactory<Building, Cell, BuildBuildingCommand> { }
	}
}