using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionsBordersCalculator : IInitializable
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

		private void CollectNeighboursFor(Cell cell, Region @in)
		{
			Debug.Log($"---[{cell.Coordinates}]---");
			cell.ForEachCellAround((c) => ToggleBordersVisibility(cell, c), on: _field.Cells);
			Debug.Log("---------");
		}

		private void ToggleBordersVisibility(Cell currentCell, Cell neighbour)
		{
			Debug.Log($"\t{neighbour.Coordinates}");
		}

		private void AddNeighbour(Region region, Cell currentCell)
			=> _field.Neighborhoods.Add(region, currentCell.RelatedRegion);
	}
}