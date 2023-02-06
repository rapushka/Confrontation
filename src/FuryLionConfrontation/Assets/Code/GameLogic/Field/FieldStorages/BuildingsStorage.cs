namespace Confrontation
{
	public static class BuildingsStorage
	{
		public static CoordinatedMatrix<Building> Buildings { get; } = new(5, 5);
	}
}