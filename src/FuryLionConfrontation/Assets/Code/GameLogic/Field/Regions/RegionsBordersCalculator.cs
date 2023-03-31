using Zenject;

namespace Confrontation
{
	public class RegionsBordersCalculator : IInitializable
	{
		[Inject] protected readonly IField Field;

		public void Initialize() => Field.Cells.ForEach(PlaceBorders);

		private void PlaceBorders(Cell cell)
			=> cell.ForEachCellAround((c) => cell.Borders.SetBorderFor(c), on: Field.Cells);
	}
}