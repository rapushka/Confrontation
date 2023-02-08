using System.Collections.Generic;

namespace Confrontation.Editor
{
	public static class LevelExtensions
	{
		public static void SetSizes(this Level @this, Sizes value) => @this.SetPrivateProperty(MemberName.Sizes, value);

		public static void SetRegions(this Level @this, List<Region.Data> value)
			=> @this.SetPrivateProperty(MemberName.Regions, value);
	}
}