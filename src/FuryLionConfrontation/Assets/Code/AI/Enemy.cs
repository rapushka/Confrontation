using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly IBalanceTable _balance;

		private DecisionMaker _decisionMaker;

		public float PassedDuration { get; set; }

		public float CoolDownDuration { get; private set; }

		public void Action()
		{
			RandomizeCoolDownDuration();

			_decisionMaker.MakeDecision().Execute();
		}

		public void Loose() => MarkOurRegionsAsNeutral();

		private void RandomizeCoolDownDuration()
			=> CoolDownDuration = _balance.EnemiesStats.SecondsBetweenActions.RandomNumberInRange;

		private void MarkOurRegionsAsNeutral()
			=> _our.Regions
			       .ForEach((r) => r.MakeNeutral());

		public class Factory : PlaceholderFactory<Our, Enemy>
		{
			[Inject] private readonly DecisionMaker.Factory _decisionMakerFactory;
			[Inject] private readonly IField _field;

			public Enemy Create(Player player)
			{
				var our = new Our(_field, player);
				var enemy = base.Create(our);
				enemy._decisionMaker = _decisionMakerFactory.Create(our, player);
				return enemy;
			}
		}
	}
}