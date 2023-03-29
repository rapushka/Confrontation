namespace Confrontation
{
	public class CellsToRegionsHandler
	{
		private readonly RegionsPage _page;

		public CellsToRegionsHandler(RegionsPage page) => _page = page;

		public void ToggleCellMembershipInSelectedRegion(Cell cell)
		{
			if (_page.HasSelectedEntry)
			{
				ToggleCellMembershipInRegion(cell, _page.SelectedEntry.Region);
			}
		}

		private static void ToggleCellMembershipInRegion(Cell cell, Region to)
		{
			cell.RelatedRegion = cell.RelatedRegion != to ? to : null;
			if (cell.RelatedRegion is not null)
			{
				cell.OwnerPlayerId = cell.RelatedRegion.OwnerPlayerId;
			}
			else
			{
				cell.SetColor(Constants.NeutralRegion);
			}
		}
	}
}