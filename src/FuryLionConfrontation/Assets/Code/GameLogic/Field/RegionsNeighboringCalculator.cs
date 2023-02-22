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
					CollectNeighboursFor(region, cell);
				}
			}
		}

		private void CollectNeighboursFor(Region region, ICoordinated cell)
			=> cell.Coordinates.ForEachCellAround((c) => AddNeighbour(region, c), on: _field.Cells);

		private void AddNeighbour(Region region, Cell currentCell)
			=> _field.Neighboring.AddNeighboring(region, currentCell.RelatedRegion);
	}
}