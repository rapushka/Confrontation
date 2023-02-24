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

		public float PassedDuration { get; set; }

		public float CoolDownDuration => _balance.EnemiesStats.SecondsBetweenActions;

		public void Action()
		{
			_unitsDirector.DirectUnits();
		}

		public void Loose()
			=> _field.Regions
			         .Where((r) => r.OwnerPlayerId == _player.Id)
			         .OnlyUnique()
			         .ForEach((r) => r.MakeNeutral());


		public class Factory : PlaceholderFactory<Player, Enemy>
		{
			[Inject] private readonly UnitsDirector.Factory _unitsDirectorFactory;

			public override Enemy Create(Player player)
			{
				var enemy = base.Create(player);
				enemy._unitsDirector = _unitsDirectorFactory.Create(player);
				return enemy;
			}
		}
	}
}