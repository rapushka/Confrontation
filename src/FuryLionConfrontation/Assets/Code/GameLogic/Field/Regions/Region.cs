using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Region : ICoordinated
	{
		[Inject] private readonly IField _field;

		private Coordinates _coordinates;
		private int _ownerPlayerId;

		private IEnumerable<Cell> CellsInRegion
			=> _field.Cells.Where((c) => c is not null && c.OwnerPlayerId == OwnerPlayerId);

		public int OwnerPlayerId
		{
			get => _ownerPlayerId;
			set
			{
				_ownerPlayerId = value;
				UpdateCellsColor();
			}
		}

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Regions.Add(this);
			}
		}

		public void UpdateCellsColor()
		{
			foreach (var cell in CellsInRegion)
			{
				_field.Cells[cell.Coordinates].SetColor(cell.OwnerPlayerId);
			}
		}

		public class Factory : PlaceholderFactory<Region>
		{
			public Region Create(RegionData data)
			{
				var region = base.Create();
				region.Coordinates = data.VillageCoordinates;
				region.OwnerPlayerId = data.OwnerPlayerId;
				return region;
			}
		}
	}
}