using Zenject;

namespace Confrontation
{
	public class DecisionMaker
	{
		[Inject] private readonly BuildRandomBuildingOnRandomCellCommand _buildRandomBuildingOnRandomCellCommand;
		[Inject] private readonly DirectRandomUnitsToRandomVillageCommand _directRandomUnitsToRandomVillageCommand;
		[Inject] private readonly Our _our;

		public ICommand MakeDecision()
		{
			return UnityEngine.Random.Range(minInclusive: 0, maxExclusive: 2) == 0
				? _directRandomUnitsToRandomVillageCommand
				: _buildRandomBuildingOnRandomCellCommand;
		}
	}
}