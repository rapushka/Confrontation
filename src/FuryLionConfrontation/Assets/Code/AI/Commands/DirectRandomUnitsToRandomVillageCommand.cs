using Zenject;

namespace Confrontation
{
	public class DirectRandomUnitsToRandomVillageCommand : ICommand
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly DirectUnitsCommand.Factory _directUnitsCommandFactory;

		public void Execute()
		{
			if (_our.Units.TryPickRandom(out var randomSquad)
			    && _our.NeighboursFor(randomSquad.LocationCell).TryPickRandom(out var randomVillage))
			{
				_directUnitsCommandFactory.Create(randomSquad, randomVillage).Execute();
			}
		}
	}
}