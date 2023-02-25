using Zenject;

namespace Confrontation
{
	public class DecisionMaker
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IField _field;

		public Decision MakeDecision()
			=> UnityEngine.Random.Range(minInclusive: 0, maxExclusive: 2) == 0
				? Decision.BuildBuilding
				: Decision.DirectUnits;

		public class Factory : PlaceholderFactory<Player, DecisionMaker> { }
	}
}