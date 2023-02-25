using System;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown, IInitializable
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IBalanceTable _balance;
		[Inject] private readonly IField _field;

		private UnitsDirector _unitsDirector;
		private EnemyBuildingBuilder _buildingBuilder;
		private DecisionMaker _decisionMaker;

		private Our _our;

		public void Initialize() => _our = new Our(_field, _player.Id);

		public float PassedDuration { get; set; }

		public float CoolDownDuration { get; private set; }

		public void Action()
		{
			RandomizeCoolDownDuration();

			ChooseAction(_decisionMaker.MakeDecision()).Invoke();
		}

		private Action ChooseAction(Decision decision)
			=> decision switch
			{
				Decision.DirectUnits   => _unitsDirector.DirectUnits,
				Decision.BuildBuilding => _buildingBuilder.Build,
				var _                  => throw new ArgumentOutOfRangeException(),
			};

		public void Loose() => MarkOurRegionsAsNeutral();

		private void RandomizeCoolDownDuration()
			=> CoolDownDuration = _balance.EnemiesStats.SecondsBetweenActions.RandomNumberInRange;

		private void MarkOurRegionsAsNeutral()
			=> _field.Regions
			         .Where((r) => r.OwnerPlayerId == _player.Id)
			         .OnlyUnique()
			         .ForEach((r) => r.MakeNeutral());

		public class Factory : PlaceholderFactory<Player, Enemy>
		{
			[Inject] private readonly UnitsDirector.Factory _unitsDirectorFactory;
			[Inject] private readonly EnemyBuildingBuilder.Factory _enemyBuildingBuilderFactory;
			[Inject] private readonly DecisionMaker.Factory _decisionMakerFactory;

			public override Enemy Create(Player player)
			{
				var enemy = base.Create(player);
				enemy._unitsDirector = _unitsDirectorFactory.Create(player);
				enemy._buildingBuilder = _enemyBuildingBuilderFactory.Create(player);
				enemy._decisionMaker = _decisionMakerFactory.Create(player);
				enemy.Initialize();
				return enemy;
			}
		}
	}
}