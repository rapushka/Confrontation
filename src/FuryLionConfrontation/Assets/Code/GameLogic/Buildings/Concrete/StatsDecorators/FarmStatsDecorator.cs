namespace Confrontation
{
	public class FarmStatsDecorator : FarmLevelStats
	{
		private readonly InfluenceMediator _influence;
		private readonly Farm _farm;
		private readonly FarmLevelStats _decoratee;

		public FarmStatsDecorator(FarmLevelStats decoratee, InfluenceMediator influence, Farm farm)
		{
			_decoratee = decoratee;
			_influence = influence;
			_farm = farm;
		}

		public override float SpawnAccelerationCoefficient
		{
			get
			{
				var baseValue = _decoratee.SpawnAccelerationCoefficient;
				return _influence.Influence(baseValue, InfluenceTarget.FarmsBonus, _farm);
			}
		}
	}
}