using UnityEngine;

namespace Confrontation
{
	public class RegionsStateClickHandler
	{
		private readonly RegionsTab _tab;

		public RegionsStateClickHandler(RegionsTab tab) => _tab = tab;

		public void Handle(Cell clickedCell)
		{
			if (_tab.SelectedEntry == false)
			{
				return;
			}

			if (clickedCell.RelatedRegion != _tab.SelectedEntry.Region)
			{
				_tab.Field.Regions[clickedCell.Coordinates] = clickedCell.RelatedRegion;
				Debug.Log($"cell on {clickedCell.Coordinates} added to region");
			}
			else
			{
				_tab.Field.Regions.Remove(clickedCell.RelatedRegion);
				Debug.Log($"cell on {clickedCell.Coordinates} removed from region");
			}
		}
	}
}