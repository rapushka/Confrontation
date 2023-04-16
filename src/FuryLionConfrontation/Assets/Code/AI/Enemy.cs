using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown
	{
		[Inject] private readonly Our _our;
		[Inject] private readonly IStatsTable _stats;
		[Inject] private readonly DecisionMaker _decisionMaker;

		public float PassedDuration { get; set; }

		public float CoolDownDuration { get; private set; }

		public void Action()
		{
			RandomizeCoolDownDuration();

			_decisionMaker.MakeDecision().Execute();
		}

		public void Loose() => MakeOurRegionsNeutral();

		private void RandomizeCoolDownDuration()
			=> CoolDownDuration = _stats.EnemiesStats.DurationRangeBetweenActions.RandomNumberInRange;

		private void MakeOurRegionsNeutral() => _our.Regions.ForEach((r) => r.MakeNeutral());

		public class Factory : PlaceholderFactory<Player, Enemy> { }
	}
}