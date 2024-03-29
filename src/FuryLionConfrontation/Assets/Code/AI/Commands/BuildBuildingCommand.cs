using Zenject;

namespace Confrontation
{
	public class BuildBuildingCommand : IEnemyCommand
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly Cell _cell;
		[Inject] private readonly Player _player;
		[Inject] private readonly IPurchase _purchase;

		public void Execute() => _purchase.BuyBuilding(_player, _building, _cell);

		public class Factory : PlaceholderFactory<Building, Cell, BuildBuildingCommand> { }
	}
}