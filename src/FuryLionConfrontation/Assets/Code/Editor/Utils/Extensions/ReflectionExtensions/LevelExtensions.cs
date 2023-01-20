using System.Collections.Generic;

namespace Confrontation.Editor
{
	public static class LevelExtensions
	{
		public static void SetRegions(this Level @this, List<Region> value)
			=> @this.SetPrivateProperty(MemberName.Regions, value);
	}
}