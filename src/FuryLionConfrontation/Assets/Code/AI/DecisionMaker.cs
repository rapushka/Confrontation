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
			if (_our.CanBeBoughtBuildings.TryPickRandom(out var building)
			    && _our.EmptyCells.TryPickRandom(out var emptyCell))
			{
				return _buildBuildingCommandFactory.Create(building, emptyCell);
			}

			if (_our.Units.TryPickRandom(out var randomSquad)
			    && _our.NeighboursFor(randomSquad).TryPickRandom(out var randomVillage))
			{
				return _directUnitsCommandFactory.Create(randomSquad, randomVillage);
			}

			return new DoNothingCommand();
		}

		private class DoNothingCommand : ICommand
		{
			public void Execute() { }
		}
	}
}