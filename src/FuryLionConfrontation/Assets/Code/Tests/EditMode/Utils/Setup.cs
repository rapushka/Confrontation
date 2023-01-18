namespace Confrontation.Tests
{
	public static class Setup
	{
		public static Field Field(int height = 20, int width = 10) => Create.Field(Create.CellPrefab(), height, width);
	}
}