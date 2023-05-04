namespace Confrontation
{
	public class ForgeStatsDecorator : ForgeLevelStats
	{
		private readonly InfluenceMediator _influence;
		private readonly Forge _forge;
		private readonly ForgeLevelStats _decoratee;

		public ForgeStatsDecorator(ForgeLevelStats decoratee, InfluenceMediator influence, Forge forge)
		{
			_decoratee = decoratee;
			_influence = influence;
			_forge = forge;
		}

		public override float CombatStrengthIncreasesRate
		{
			get
			{
				var baseValue = _decoratee.CombatStrengthIncreasesRate;
				return _influence.Influence(baseValue, InfluenceTarget.ForgesBonus, _forge);
			}
		}
	}
}