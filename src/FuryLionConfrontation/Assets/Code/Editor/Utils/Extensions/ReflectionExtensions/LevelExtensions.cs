using System.Collections.Generic;

namespace Confrontation.Editor
{
	public static class LevelExtensions
	{
		public static void SetRegions(this Level @this, List<Village.Data> value)
			=> @this.SetPrivateProperty(MemberName.Regions, value);
	}
}