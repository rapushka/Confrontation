using Zenject;

namespace Confrontation
{
	public class DirectRandomUnitsToRandomVillageCommand : ICommand
	{
		[Inject] private readonly Our _our;

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