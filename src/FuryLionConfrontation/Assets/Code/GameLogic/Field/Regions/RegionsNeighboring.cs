namespace Confrontation
{
	public class RegionsNeighboring
	{
		public RegionsNeighboring(Sizes fieldSizes) => Neighborhoods = fieldSizes.CreateMatrix<bool>();

		public bool[,] Neighborhoods { get; }

		public bool IsNeighbours(Region first, Region second) => Neighborhoods[first.Id, second.Id];

		public void AddNeighboring(Region first, Region second)
		{
			Neighborhoods[first.Id, second.Id] = true;
			Neighborhoods[second.Id, first.Id] = true;
		}
	}
}