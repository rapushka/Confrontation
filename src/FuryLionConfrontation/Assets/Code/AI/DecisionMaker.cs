using Zenject;

namespace Confrontation
{
	public class DecisionMaker
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly Player _player;
		[Inject] private readonly Purchase _purchase;

		public ICommand MakeDecision()
			=> UnityEngine.Random.Range(minInclusive: 0, maxExclusive: 2) == 0
				? new DirectRandomUnitsToRandomVillageCommand(_our)
				: new BuildRandomBuildingOnRandomCellCommand(_our, _player, _purchase);

		public class Factory : PlaceholderFactory<Our, Player, DecisionMaker> { }
	}
}