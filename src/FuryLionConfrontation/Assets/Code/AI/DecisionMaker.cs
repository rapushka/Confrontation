using Zenject;

namespace Confrontation
{
	public class DecisionMaker
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly DirectUnitsCommand.Factory _directUnitsCommandFactory;
		[Inject] private readonly BuildBuildingCommand.Factory _buildBuildingCommandFactory;

		public ICommand MakeDecision()
		{
			if (_our.CanBuyPreferredBuilding(out var building)
			    && _our.EmptyCells.TryPickRandom(out var emptyCell))
			{
				return _buildBuildingCommandFactory.Create(building, emptyCell);
			}

			if (_our.Units.TryPickRandom(out var squad)
			    && squad == true
			    && _our.NeighboursFor(squad.LocationCell).TryPickRandom(out var placeable))
			{
				return _directUnitsCommandFactory.Create(squad, placeable);
			}

			return new DoNothingCommand();
		}

		private class DoNothingCommand : ICommand
		{
			public void Execute() { }
		}
	}
}