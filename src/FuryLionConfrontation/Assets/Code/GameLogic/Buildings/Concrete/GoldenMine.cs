namespace Confrontation
{
	public class GoldenMine : ResourcesGenerator
	{
		public override string Name => nameof(GoldenMine);

		protected override LeveledStats<GeneratorStatsBase> Stats => StatsTable.GoldenMineStats.LeveledStats;

		protected override void Produce() => OwnerPlayer?.Resources.Gold.Earn(ProducingRate);
	}
}