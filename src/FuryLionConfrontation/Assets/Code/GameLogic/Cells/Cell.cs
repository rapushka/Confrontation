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

		[CanBeNull] public Garrison Garrison => _field.Garrisons[Coordinates];

		[CanBeNull] public Building Building => _field.Buildings[Coordinates];

		[CanBeNull] public Region RelatedRegion
		{
			get => _field.Regions[Coordinates];
			set => _field.Regions[Coordinates] = value;
		}

		public int OwnerPlayerId
		{
			get => _field.Regions[Coordinates]?.OwnerPlayerId ?? -1;
			set => _field.Regions[Coordinates].OwnerPlayerId = value;
		}

		public Cell CellWithVillage => _field.Cells[_field.Regions[Coordinates].Coordinates];

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

		public void MakeRegionNeutral()
		{
			if (Building is not Capital)
			{
				RelatedRegion!.OwnerPlayerId = Constants.NeutralRegion;
			}
		}

		public void SetColor(int playerId) => _color.ChangeColorTo(playerId);

		public void DetachUnitsSquad() => _field.LocatedUnits.Remove(LocatedUnits);

		public class Factory : PlaceholderFactory<Cell>
		{
			[Inject] private readonly IAssetsService _assets;

			public override Cell Create()
			{
				var cell = base.Create();
				_assets.ToGroup(cell.transform, InstantiateGroup.Cells);
				return cell;
			}
		}
	}
}