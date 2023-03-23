using Zenject;

namespace Confrontation
{
	public class RegionsStateClickHandler
	{
		private readonly RegionsTab _tab;

		public RegionsStateClickHandler(RegionsTab tab) => _tab = tab;

		public void Handle(Cell clickedCell)
		{
			if (_tab.SelectedEntry == true)
			{
				clickedCell.RelatedRegion = clickedCell.RelatedRegion != _tab.SelectedEntry.Region
					? _tab.SelectedEntry.Region
					: null;
			}
		}
	}
}