namespace Confrontation.Editor
{
	public static class RegionExtensions
	{
		public static void SetVillageCoordinates(this Village.Data @this, Coordinates value)
			=> @this.SetPrivateProperty(MemberName.VillageCoordinates, value);
	}
}