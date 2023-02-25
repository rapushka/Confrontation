namespace Confrontation
{
	public class DirectUnitsCommand : ICommand
	{
		private readonly UnitsSquad _squad;
		private readonly Village _village;

		public DirectUnitsCommand(UnitsSquad squad, Village village)
		{
			_squad = squad;
			_village = village;
		}

		public void Execute() => _squad.MoveTo(_village.RelatedCell);
	}
}