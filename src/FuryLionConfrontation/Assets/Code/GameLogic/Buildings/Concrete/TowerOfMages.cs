namespace Confrontation
{
	public class TowerOfMages : ResourcesGenerator
	{
		public override string Name => "Tower Of Mages";

		protected override LeveledStats<GeneratorStatsBase> Stats => StatsTable.TowerOfMagesStats.LeveledStats;

		protected override void Produce() => OwnerPlayer?.Resources.Mana.Earn(ProducingRate);
	}
}