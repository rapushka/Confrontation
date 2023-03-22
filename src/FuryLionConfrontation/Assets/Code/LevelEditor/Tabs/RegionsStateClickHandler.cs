using UnityEngine;

namespace Confrontation
{
	public class RegionsStateClickHandler : IFieldClickHandler
	{
		private readonly RegionsTab _tab;

		public RegionsStateClickHandler(RegionsTab tab) => _tab = tab;

		public void Handle(Cell clickedCell)
		{
			if (_tab.SelectedEntry == false)
			{
				return;
			}

			if (clickedCell.RelatedRegion == _tab.SelectedEntry.Region)
			{
				_tab.Field.Regions.Remove(clickedCell.RelatedRegion);
			}
			else
			{
				_tab.Field.Regions.Add(clickedCell.RelatedRegion);
				Debug.Log($"cell on {clickedCell.Coordinates} now in region {clickedCell.RelatedRegion!.Id}");
			}
		}
	}
}