using Zenject;

namespace Confrontation
{
	public class DirectUnitsCommand : ICommand
	{
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly Settlement _settlement;

		public void Execute() => _squad.MoveTo(_settlement.RelatedCell);

		public class Factory : PlaceholderFactory<UnitsSquad, Settlement, DirectUnitsCommand> { }
	}
}