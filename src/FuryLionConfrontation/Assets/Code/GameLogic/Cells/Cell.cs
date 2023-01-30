using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;

		[CanBeNull] public UnitsSquad UnitsSquads { get; set; }

		private Coordinates _coordinates;

		[CanBeNull] public Building Building { get; set; }

		public bool IsEmpty   => Building is null;
		public bool HaveUnits => UnitsSquads is not null;

		public void SetColor(int playerId) => _color.ChangeColorTo(playerId);

		public Village RelatedRegion { get; set; }

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				transform.position = value.CalculatePosition().AsTopDown();
				_coordinates = value;
			}
		}

		public bool IsBelongTo(int playerId)
			=> RelatedRegion is not null && RelatedRegion.OwnerPlayerId == playerId;
	}
}