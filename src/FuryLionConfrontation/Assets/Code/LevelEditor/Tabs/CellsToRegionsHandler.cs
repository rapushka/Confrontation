namespace Confrontation
{
	public class CellsToRegionsHandler
	{
		private readonly RegionsTab _tab;

		public CellsToRegionsHandler(RegionsTab tab) => _tab = tab;

		public void Add(Cell cell)
		{
			if (_tab.SelectedEntry == true)
			{
				Add(cell, _tab.SelectedEntry.Region);
			}
		}

		private void Add(Cell cell, Region to) => cell.RelatedRegion = cell.RelatedRegion != to ? to : null;
	}
}