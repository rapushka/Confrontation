using Zenject;

namespace Confrontation
{
	public class Barracks : Building, IActorWithCoolDown
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly IBalanceTable _balanceTable;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => Balance.GenerationCoolDown;

		public override string Name => nameof(Barracks);

		protected override int MaxLevel => BalanceTable.Barrack.MaxLevel;

		private bool HaveSquad => LocatedUnits is not null;

		private UnitsSquad LocatedUnits => Field.LocatedUnits[Coordinates];

		private BarrackBalanceData Balance => _balanceTable.Barrack[Level];

		public void Action()
		{
			for (var i = 0; i < Balance.GenerationAmount; i++)
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