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

		public virtual void Action()
		{
			for (var i = 0; i < CurrentLevelStats.Amount; i++)
			{
				SpawnGarrison();
			}
		}

		private void SpawnGarrison()
		{
			if (HaveGarrison)
			{
				if (LocatedGarrison.QuantityOfUnits < CurrentLevelStats.MaxInGarrisonNumber)
				{
					LocatedGarrison.QuantityOfUnits++;
				}
			}
			else
			{
				_garrisonsFactory.Create(RelatedCell);
			}
		}
	}
}