using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;

		[CanBeNull] public UnitsSquad UnitsSquads { get; set; }

		[CanBeNull] public Building Building { get; set; }

		public bool IsEmpty   => Building is null;
		public bool HaveUnits => UnitsSquads is not null;

		public void SetColor(int playerId) => _color.ChangeColorTo(playerId);

		public Village RelatedRegion { get; set; }

		public Coordinates Coordinates
		{
			set => transform.position = value.CalculatePosition().AsTopDown();
		}

		public bool IsBelongTo(Player player)
			=> RelatedRegion is not null && RelatedRegion.OwnerPlayerId == player.Id;

		public void MakeRegionNeutral() => RelatedRegion.SetOwner(Constants.NeutralRegion);

	}
}