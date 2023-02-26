using Zenject;

namespace Confrontation
{
	public class DecisionMaker
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly DirectUnitsCommand.Factory _directUnitsCommandFactory;
		[Inject] private readonly BuildBuildingCommand.Factory _buildBuildingCommandFactory;
		[Inject] private readonly IBalanceTable _balance;
		[Inject] private readonly Player _player;

		public ICommand MakeDecision()
		{
			var randomBuilding = _balance.EnemiesStats.BuildingsPriority.PickRandom().Prefab;
			if (_player.Stats.IsEnoughGoldFor(_balance.PriceFor(randomBuilding))
			    && _our.EmptyCells.TryPickRandom(out var emptyCell))
			{
				return _buildBuildingCommandFactory.Create(randomBuilding, emptyCell);
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