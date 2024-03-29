using Zenject;

namespace Confrontation
{
	public class Barrack : Generator
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly IStatsTable _statsTable;

		public override float PassedDuration { get; set; }

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		public override string Name => nameof(Barrack);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		protected float MinAcceleratedCoolDown => CurrentLevelStats.MinAcceleratedCoolDown;

		private LeveledStats<BarrackLevelStats> Stats => StatsTable.BarrackStats.LeveledStats;

		private bool HaveSquad => LocatedUnits == true;

		private UnitsSquad LocatedUnits => Field.LocatedUnits[Coordinates];

		private BarrackLevelStats CurrentLevelStats => _statsTable.BarrackStats.LeveledStats[Level];

		private UnitsSquad ActualUnitsSquad => HaveSquad ? LocatedUnits : _unitsFactory.Create(RelatedCell);

		public override void Action() => SpawnUnits();

		private void SpawnUnits() => ActualUnitsSquad.QuantityOfUnits += CurrentLevelStats.Amount;
	}
}