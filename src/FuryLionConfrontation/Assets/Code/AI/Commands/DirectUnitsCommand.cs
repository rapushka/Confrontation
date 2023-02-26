using Zenject;

namespace Confrontation
{
	public class DirectUnitsCommand : ICommand
	{
		[Inject] private readonly UnitsSquad _squad;
		[Inject] private readonly Village _village;

		public void Execute() => _squad.MoveTo(_village.RelatedCell);

		public class Factory : PlaceholderFactory<UnitsSquad, Village, DirectUnitsCommand> { }
	}
}