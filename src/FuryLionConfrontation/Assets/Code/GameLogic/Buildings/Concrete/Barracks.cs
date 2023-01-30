using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Barracks : Building, IActorWithCoolDown
	{
		[Inject] private readonly IAssetsService _assets;

		[field: SerializeField] public float CoolDownDuration { get; private set; } = 10f;

		[field: SerializeField] public UnitsSquad UnitPrefab { get; private set; }

		public float PassedDuration { get; set; }

		private bool HaveSquad => RelatedCell.UnitsSquads == true;

		private Vector3 InitialUnitPosition => transform.position + Constants.VerticalOffsetAboveCell;

		public void Action() => CreateUnit();

		private void CreateUnit()
		{
			if (HaveSquad)
			{
				RelatedCell.UnitsSquads!.QuantityOfUnits++;
			}
			else
			{
				CreateNewSquad();
			}
		}

		private void CreateNewSquad()
		{
			var squad = _assets.Instantiate(UnitPrefab, InitialUnitPosition);
			squad.OwnerPlayerId = RelatedCell.RelatedRegion.OwnerPlayerId;
			squad!.SetLocation(RelatedCell);
			squad.QuantityOfUnits = 1;
		}
	}
}