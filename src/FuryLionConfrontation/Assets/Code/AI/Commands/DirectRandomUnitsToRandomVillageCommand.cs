namespace Confrontation
{
	public class DirectRandomUnitsToRandomVillageCommand : ICommand
	{
		private readonly Our _our;

		private DirectUnitsCommand _directUnitsCommand;

		public DirectRandomUnitsToRandomVillageCommand(Our our) => _our = our;

		public void Execute()
		{
			if (_our.Units.TryPickRandom(out var randomSquad)
			    && _our.NeighboursFor(randomSquad).TryPickRandom(out var randomVillage))
			{
				new DirectUnitsCommand(randomSquad, randomVillage).Execute();
			}
		}
	}
}