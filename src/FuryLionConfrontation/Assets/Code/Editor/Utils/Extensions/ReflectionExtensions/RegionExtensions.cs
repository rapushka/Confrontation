namespace Confrontation.Editor
{
	public static class RegionExtensions
	{
		public static void SetVillageCoordinates(this RegionData @this, Coordinates value)
			=> @this.SetPrivateProperty(MemberName.VillageCoordinates, value);
	}
}