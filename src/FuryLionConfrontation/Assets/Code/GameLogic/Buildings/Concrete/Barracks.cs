using Zenject;

namespace Confrontation
{
	public class Barracks : Building, IActorWithCoolDown
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly IBalanceTable _balanceTable;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => CurrentLevelStats.CoolDown;

		public override string Name => nameof(Barracks);

		protected override int MaxLevel => BalanceTable.BarrackStats.LeveledStats.MaxLevel;

		private bool HaveSquad => LocatedUnits is not null;

		private UnitsSquad LocatedUnits => Field.LocatedUnits[Coordinates];

		private GeneratorStatsBase CurrentLevelStats => _balanceTable.BarrackStats.LeveledStats[Level];

		public void Action()
		{
			for (var i = 0; i < CurrentLevelStats.Amount; i++)
			{
				SpawnUnit();
			}
		}

		private void SpawnUnit()
		{
			if (HaveSquad)
			{
				LocatedUnits.QuantityOfUnits++;
			}
			else
			{
				_unitsFactory.Create(RelatedCell, RelatedCell.OwnerPlayerId);
			}
		}
	}
}