using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IBalanceTable _balance;
		[Inject] private readonly IField _field;

		private UnitsDirector _unitsDirector;
		private EnemyBuildingBuilder _buildingBuilder;

		public float PassedDuration { get; set; }

		public float CoolDownDuration { get; private set; }

		public void Action()
		{
			RandomizeCoolDownDuration();

			if (UnityEngine.Random.Range(minInclusive: 0, maxExclusive: 2) == 0)
			{
				_unitsDirector.DirectUnits();
			}
			else
			{
				_buildingBuilder.Build();
			}
		}

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

			public override Enemy Create(Player player)
			{
				var enemy = base.Create(player);
				enemy._unitsDirector = _unitsDirectorFactory.Create(player);
				enemy._buildingBuilder = _enemyBuildingBuilderFactory.Create(player);
				return enemy;
			}
		}
	}
}