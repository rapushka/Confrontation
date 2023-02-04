using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor
{
	public static class FieldExtensions
	{
		public static IEnumerable<Village> GetVillages(this ConfigurableField @this)
			=> @this.Cells.Select((c) => c.Building).OfType<Village>();
	}
}