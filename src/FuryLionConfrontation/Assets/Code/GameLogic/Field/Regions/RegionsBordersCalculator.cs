using Zenject;

namespace Confrontation
{
	public class RegionsBordersCalculator : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize()
		{
			foreach (var cell in _field.Cells)
			{
				PlaceBordersFor(cell);
			}
		}

		private void PlaceBordersFor(Cell cell)
			=> cell.ForEachCellAround((c) => cell.Borders.SetBorderFor(c), on: _field.Cells);
	}
}