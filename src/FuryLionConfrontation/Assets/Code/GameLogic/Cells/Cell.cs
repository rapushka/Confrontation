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

		[CanBeNull] public UnitsSquad UnitsSquads => _field.LocatedUnits[Coordinates];

		[CanBeNull] public Building Building => _field.Buildings[Coordinates];

		public Village RelatedRegion { get; set; }

		public bool IsEmpty => Building is null;

		public bool HasUnits => UnitsSquads is not null;

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
			var building = _field.Buildings[Coordinates];
			if (building == true)
			{
				building.OwnerPlayerId = newOwnerId;
			}
		}

		public class Factory : PlaceholderFactory<Cell> { }
	}
}