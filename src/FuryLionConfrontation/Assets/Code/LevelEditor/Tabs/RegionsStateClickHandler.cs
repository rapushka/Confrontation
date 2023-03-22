using cakeslice;
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
				_tab.Field.Regions[clickedCell.Coordinates] = _tab.SelectedEntry.Region;
				clickedCell.GetComponentInChildren<MeshRenderer>().gameObject.AddComponent<Outline>();
			}
			else
			{
				_tab.Field.Regions[clickedCell.Coordinates] = null;
				clickedCell.GetComponentInChildren<MeshRenderer>().gameObject.DestroyComponent<Outline>();
			}
		}
	}
}