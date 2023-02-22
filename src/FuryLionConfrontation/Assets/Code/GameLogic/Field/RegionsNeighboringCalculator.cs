using Zenject;

namespace Confrontation
{
	public class RegionsNeighboringCalculator : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize()
		{
			foreach (var region in _field.Regions.OnlyUnique())
			{
				foreach (var cell in region.CellsInRegion)
				{
					CollectNeighboursFor(cell, @in: region);
				}
			}
		}

		private void CollectNeighboursFor(ICoordinated cell, Region @in)
			=> cell.ForEachCellAround((c) => AddNeighbour(@in, c), on: _field.Cells);

		private void AddNeighbour(Region region, Cell currentCell)
			=> _field.Neighboring.AddNeighboring(region, currentCell.RelatedRegion);
	}
}