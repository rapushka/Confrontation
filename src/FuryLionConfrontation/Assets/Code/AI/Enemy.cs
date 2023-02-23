using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IBalanceTable _balance;
		[Inject] private readonly IField _field;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => _balance.EnemiesStats.SecondsBetweenActions;

		private UnitsSquad[] OurUnits => _field.LocatedUnits.Where(IsOurUnit).AsArray();

		public void Action()
		{
			DirectUnits();
		}

		private void DirectUnits()
		{
			if (OurUnits.TryPickRandom(out var randomSquad)
			    && CollectNeighboursFor(randomSquad).TryPickRandom(out var randomVillage))
			{
				randomSquad.Move(randomSquad.LocationCell, randomVillage.RelatedCell);
			}
		}

		private IEnumerable<Village> CollectNeighboursFor(UnitsSquad randomSquad)
			=> _field.Buildings
			         .OfType<Village>()
			         .Where((v) => IsNeighbours(v, randomSquad.LocationCell.RelatedRegion));

		private bool IsOurUnit(UnitsSquad unit) => unit is not null && unit.OwnerPlayerId == _player.Id;

		private bool IsNeighbours(Village village, Region currentRegion)
			=> _field.Neighboring.IsNeighbours(village.RelatedCell.RelatedRegion, currentRegion);

		public class Factory : PlaceholderFactory<Player, Enemy> { }
	}
}