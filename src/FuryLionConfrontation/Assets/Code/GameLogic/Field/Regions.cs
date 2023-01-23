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
			=> region.Cells.Select((c) => _field.Cells[c]).ForEach((c) => BindCellToRegion(region, c));

		private void BindCellToRegion(Region region, Cell cell)
		{
			var village = CreateVillage(region);
			village.CellsInRegion.Add(cell);
			cell.RelatedRegion = village;
		}

		private Village CreateVillage(Region region)
		{
			var cell = _field.Cells[region.Coordinates];
			return _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform)
			              .With((v) => v.OwnerPlayerId = region.OwnerPlayerId)
			              .With((v) => cell.Building = v);
		}
	}
}