using System;
using Zenject;

namespace Confrontation
{
	public class Settlement : Generator, IPlaceable
	{
		[Inject] private readonly Garrison.Factory _garrisonsFactory;

		public override float PassedDuration { get; set; }

		public override string Name => nameof(Settlement);

		public override int UpgradePrice => Stats.UpgradePrice;

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<SettlementLevelStats> Stats => BalanceTable.SettlementStats.LeveledStats;

		private Garrison LocatedGarrison => Field.Garrisons[Coordinates];

		private int MaxInGarrisonNumber => CurrentLevelStats.MaxInGarrisonNumber;

		private int NewInGarrisonNumber => ActualGarrison.QuantityOfUnits + CurrentLevelStats.Amount;

		private SettlementLevelStats CurrentLevelStats => BalanceTable.SettlementStats.LeveledStats[Level];

		private Garrison ActualGarrison => HaveGarrison ? LocatedGarrison : _garrisonsFactory.Create(RelatedCell);

		private bool HaveGarrison => LocatedGarrison == true;

		public override void Action() => SpawnGarrison();

		private void SpawnGarrison()
			=> ActualGarrison.QuantityOfUnits = Math.Min(NewInGarrisonNumber, MaxInGarrisonNumber);
	}
}