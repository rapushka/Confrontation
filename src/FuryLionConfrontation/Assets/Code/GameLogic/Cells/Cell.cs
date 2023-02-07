using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Cell : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;

		[SerializeField] private RegionColor _color;

		private Coordinates _coordinates;

		[CanBeNull] public UnitsSquad LocatedUnits => _field.LocatedUnits[Coordinates];

		[CanBeNull] public Building Building => _field.Buildings[Coordinates];

		public int OwnerPlayerId
		{
			get => _field.Regions[Coordinates].OwnerPlayerId;
			set => _field.Regions[Coordinates].OwnerPlayerId = value;
		}

		public Village RelatedRegion { get; set; }

		public bool IsEmpty => Building is null;

		public bool HasUnits => LocatedUnits is not null;

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Cells.Add(this);
				transform.position = _coordinates.CalculatePosition().AsTopDown();
			}
		}

		public bool IsBelongTo(Player player) => OwnerPlayerId == player.Id;

		public void MakeRegionNeutral() => RelatedRegion.SetOwner(Constants.NeutralRegion);

		public void SetColor(int playerId) => _color.ChangeColorTo(playerId);

		public class Factory : PlaceholderFactory<Cell> { }
	}
}