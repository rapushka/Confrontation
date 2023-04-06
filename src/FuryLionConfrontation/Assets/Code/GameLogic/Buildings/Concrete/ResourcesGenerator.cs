using Zenject;

namespace Confrontation
{
	public abstract class ResourcesGenerator : Generator
	{
		[Inject] private readonly ISession _gameSession;

		public override float PassedDuration { get; set; }

		protected Player OwnerPlayer => _gameSession.GetPlayerFor(OwnerPlayerId);

		public override int UpgradePrice => Stats.UpgradePrice;

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		protected override int MaxLevel => Stats.MaxLevel;

		protected int ProducingRate => CurrentLevelStats.Amount;

		protected abstract LeveledStats<GeneratorStatsBase> Stats { get; }

		private GeneratorStatsBase CurrentLevelStats => Stats[Level];

		public override void Action() => Produce();

		protected abstract void Produce();
	}
}