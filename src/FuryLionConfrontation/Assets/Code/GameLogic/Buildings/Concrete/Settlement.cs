using System;
using Zenject;

namespace Confrontation
{
	public class Settlement : Building, IActorWithCoolDown
	{
		[Inject] private readonly Garrison.Factory _garrisonsFactory;

		public float PassedDuration { get; set; }

		public override string Name => nameof(Settlement);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<SettlementLevelStats> Stats => BalanceTable.SettlementStats.LeveledStats;

		public float CoolDownDuration => CurrentLevelStats.CoolDown;

		private SettlementLevelStats CurrentLevelStats => BalanceTable.SettlementStats.LeveledStats[Level];

		private bool HaveGarrison => LocatedGarrison == true;

		private Garrison LocatedGarrison => Field.Garrisons[Coordinates];

		private Garrison ActualGarrison => HaveGarrison ? LocatedGarrison : _garrisonsFactory.Create(RelatedCell);

		private int MaxInGarrisonNumber => CurrentLevelStats.MaxInGarrisonNumber;

		private int NewInGarrisonNumber => ActualGarrison.QuantityOfUnits + CurrentLevelStats.Amount;

		public virtual void Action() => SpawnGarrison();

		private void SpawnGarrison()
			=> ActualGarrison.QuantityOfUnits = Math.Min(NewInGarrisonNumber, MaxInGarrisonNumber);
	}
}