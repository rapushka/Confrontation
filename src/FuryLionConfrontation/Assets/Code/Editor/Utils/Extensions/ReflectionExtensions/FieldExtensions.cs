using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class FieldExtensions
	{
		public static void AddCellToFirstRegion(this Field @this, Cell cell)
			=> @this.GetRegions().First().Cells.Add(cell.Coordinates);

		public static void AddRegion(this Field @this, Village.Data villageData) => @this.GetRegions().Add(villageData);

		public static List<Village.Data> GetRegions(this Field @this)
			=> @this.GetLevel().GetPropertyValue<List<Village.Data>>(MemberName.Regions);

		public static Level GetLevel(this Field @this)
			=> @this.GetPrivateField<IResourcesService>(MemberName.Resources).GetPropertyValue<Level>(MemberName.CurrentLevel);

		public static IEnumerable<Village> GetVillages(this Field @this)
			=> @this.GetCells().Select((c) => c.Building).OfType<Village>();

		public static IEnumerable<Cell> GetCells(this Field @this)
			=> @this.GetPrivateField<Cell[,]>(MemberName.Cells).Cast<Cell>();

		public static Transform GetRoot(this Field @this) => @this.GetPrivateField<Transform>(MemberName.Root);
	}
}