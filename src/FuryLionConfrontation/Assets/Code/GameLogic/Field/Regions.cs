using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Regions : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IAssetsService _assets;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _resources.CurrentLevel.Regions.ForEach(ToRegion);

		private void ToRegion(Region region)
		{
			var village = CreateVillage(region);
			region.Cells.Select((c) => _field.Cells[c]).ForEach((c) => BindCellToRegion(village, c));
		}

		private Village CreateVillage(Region region)
		{
			var ownerCell = _field.Cells[region.Coordinates];
			var village = _assets.Instantiate(original: _resources.VillagePrefab, parent: ownerCell.transform);
			ownerCell.Building = village;
			return village;
		}

		private void BindCellToRegion(Village village, Cell cell)
		{
			cell.RelatedRegion = village;
			village.CellsInRegion.Add(cell);
		}
	}
}