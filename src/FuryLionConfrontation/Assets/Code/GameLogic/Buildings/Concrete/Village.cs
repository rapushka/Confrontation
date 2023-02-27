using Zenject;

namespace Confrontation
{
	public class Village : Building, IActorWithCoolDown
	{
		[Inject] private readonly Garrison.Factory _garrisonsFactory;

		public float PassedDuration { get; set; }

		public override string Name => nameof(Village);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<VillageLevelStats> Stats => BalanceTable.VillageStats.LeveledStats;

		public float CoolDownDuration => CurrentLevelStats.CoolDown;

		private VillageLevelStats CurrentLevelStats => BalanceTable.VillageStats.LeveledStats[Level];

		private bool HaveGarrison => LocatedGarrison == true;

		private Garrison LocatedGarrison => Field.Garrisons[Coordinates];

		private Garrison ActualGarrison => HaveGarrison ? LocatedGarrison : _garrisonsFactory.Create(RelatedCell);

		public virtual void Action() => SpawnGarrison();

		private void SpawnGarrison()
		{
			if (ActualGarrison.QuantityOfUnits < CurrentLevelStats.MaxInGarrisonNumber)
			{
				ActualGarrison.QuantityOfUnits += CurrentLevelStats.Amount;
			}
		}
	}
}