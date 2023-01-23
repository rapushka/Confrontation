using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class FieldExtensions
	{
		public static void AddCellToFirstRegion(this Field @this, Cell cell)
			=> @this.GetRegions().First().CellsCoordinates.Add(cell.Coordinates);

		public static void AddRegion(this Field @this, Region villageData) => @this.GetRegions().Add(villageData);

		public static List<Region> GetRegions(this Field @this)
			=> @this.GetLevel().GetPropertyValue<List<Region>>(MemberName.Regions);

		public static Level GetLevel(this Field @this)
			=> @this.GetPrivateField<IResourcesService>(MemberName.Resources).GetPropertyValue<Level>(MemberName.CurrentLevel);

		public static IEnumerable<Village> GetVillages(this Field @this)
			=> @this.GetCells().Select((c) => c.Building).OfType<Village>();

		public static IEnumerable<Cell> GetCells(this Field @this)
			=> @this.GetPropertyValue<CoordinatedMatrix<Cell>>(MemberName.Cells);

		public static Transform GetRoot(this Field @this) => @this.GetPrivateField<Transform>(MemberName.Root);
	}
}