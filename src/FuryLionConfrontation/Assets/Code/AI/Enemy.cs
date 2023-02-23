using System.Linq;
using UnityEngine.Assertions;
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

		public void Action()
		{
			DirectUnits();
		}

		private void DirectUnits()
		{
			if (_field.LocatedUnits.Any(IsOurUnit) == false)
			{
				return;
			}

			Assert.IsNotNull(_field.LocatedUnits);
			var ourUnits = _field.LocatedUnits.Where(IsOurUnit);

			var randomSquad = ourUnits.PickRandom();
			var currentRegion = randomSquad.LocationCell.RelatedRegion;

			var neighbourVillages = _field.Buildings
			                              .OfType<Village>()
			                              .Where((v) => IsNeighbours(v, currentRegion));

			var enumerable = neighbourVillages as Village[] ?? neighbourVillages.ToArray();
			if (enumerable.Any() == false)
			{
				return;
			}

			var randomVillage = enumerable.PickRandom();

			randomSquad.Move(randomSquad.LocationCell, randomVillage.RelatedCell);
		}

		private bool IsOurUnit(UnitsSquad u)
		{
			return u is not null && u.OwnerPlayerId == _player.Id;
		}

		private bool IsNeighbours(Village village, Region currentRegion)
			=> _field.Neighboring.IsNeighbours(village.RelatedCell.RelatedRegion, currentRegion);

		public class Factory : PlaceholderFactory<Player, Enemy> { }
	}
}