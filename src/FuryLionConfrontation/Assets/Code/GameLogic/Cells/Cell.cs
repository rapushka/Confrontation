using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;

		[CanBeNull] public UnitsSquad UnitsSquads { get; set; }

		[CanBeNull] public Building Building { get; set; }

		public Village RelatedRegion { get; set; }

		public bool IsEmpty => Building is null;

		public bool HasUnits => UnitsSquads is not null;

		public Coordinates Coordinates
		{
			set => transform.position = value.CalculatePosition().AsTopDown();
		}

		public void SetColor(int playerId) => _color.ChangeColorTo(playerId);

		public bool IsBelongTo(Player player) => RelatedRegion is not null && RelatedRegion.OwnerPlayerId == player.Id;

		public void MakeRegionNeutral() => RelatedRegion.SetOwner(Constants.NeutralRegion);

		public void ChangeOwnerTo(int newOwnerId)
		{
			AppropriateBuildingTo(newOwnerId);
			AppropriateUnitsTo(newOwnerId);
			SetColor(newOwnerId);
		}

		private void AppropriateUnitsTo(int newOwnerId)
		{
			if (HasUnits)
			{
				UnitsSquads!.OwnerPlayerId = newOwnerId;
			}
		}

		private void AppropriateBuildingTo(int newOwnerId)
		{
			if (IsEmpty == false)
			{
				Building!.OwnerPlayerId = newOwnerId;
			}
		}
	}
}