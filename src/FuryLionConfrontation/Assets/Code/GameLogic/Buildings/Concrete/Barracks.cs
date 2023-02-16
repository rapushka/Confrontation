using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Barracks : Building, IActorWithCoolDown
	{
		[Inject] private readonly UnitsSquad.Factory _unitsFactory;
		[Inject] private readonly BalanceTable _balanceTable;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => Balance.CoolDownDuration;

		public override string Name => nameof(Barracks);

		protected override int MaxLevel => BalanceTable.Barracks.MaxLevel;

		private bool HaveSquad => LocatedUnits is not null;

		private UnitsSquad LocatedUnits => Field.LocatedUnits[Coordinates];

		private Vector3 InitialUnitPosition => transform.position + Constants.VerticalOffsetAboveCell;

		private BarrackBalanceData Balance => _balanceTable.Barracks[Level];

		public void Action()
		{
			for (var i = 0; i < Balance.SpawnAmount; i++)
			{
				CreateUnit();
			}
		}

		private void CreateUnit()
		{
			if (HaveSquad)
			{
				LocatedUnits.QuantityOfUnits++;
			}
			else
			{
				_unitsFactory.Create(InitialUnitPosition, RelatedCell, RelatedCell.OwnerPlayerId);
			}
		}
	}
}