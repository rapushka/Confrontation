using Zenject;

namespace Confrontation
{
	public class DirectUnitsCommand : IEnemyCommand
	{
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly IPlaceable _placeable;

		public void Execute() => _squad.MoveTo(_placeable.RelatedCell);

		public class Factory : PlaceholderFactory<UnitsSquad, IPlaceable, DirectUnitsCommand> { }
	}
}