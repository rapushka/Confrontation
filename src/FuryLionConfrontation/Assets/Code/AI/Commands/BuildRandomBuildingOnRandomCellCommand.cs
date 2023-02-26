using Zenject;

namespace Confrontation
{
	public class BuildRandomBuildingOnRandomCellCommand : ICommand
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly BuildBuildingCommand.Factory _buildBuildingCommandFactory;

		public void Execute()
		{
			if (_our.CanBeBoughtBuildings.TryPickRandom(out var building)
			    && _our.EmptyCells.TryPickRandom(out var emptyCell))
			{
				_buildBuildingCommandFactory.Create(building, emptyCell).Execute();
			}
		}
	}
}