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

		private IEnumerable<Cell> CellsInRegion => _field.Cells.Where((c) => c.OwnerPlayerId == OwnerPlayerId);

		public int OwnerPlayerId
		{
			get => _ownerPlayerId;
			set
			{
				_ownerPlayerId = value;
				UpdateCellsColor();
				UpdateOwnerOfUnitsInRegion();
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
				cell.SetColor(OwnerPlayerId);
			}
		}

		
		private void UpdateOwnerOfUnitsInRegion()
		{
			foreach (var cellInRegion in _field.Cells.Where((c) => c.RelatedRegion == this))
			{
				if (cellInRegion.LocatedUnits is not null)
				{
					cellInRegion.LocatedUnits!.OwnerPlayerId = OwnerPlayerId;
				}
			}
		}

		public class Factory : PlaceholderFactory<Region>
		{
			public Region Create(RegionData data)
			{
				var region = base.Create();
				region.OwnerPlayerId = data.OwnerPlayerId;
				region.Coordinates = data.VillageCoordinates;
				return region;
			}
		}
	}
}