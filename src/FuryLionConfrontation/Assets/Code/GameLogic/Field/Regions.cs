using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Regions : IInitializable
	{
		private readonly Field _field;
		private readonly IResourcesService _resources;
		private readonly IAssetsService _assets;

		[Inject]
		public Regions(Field field, IResourcesService resources, IAssetsService assets)
		{
			_field = field;
			_resources = resources;
			_assets = assets;
		}

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _resources.CurrentLevel.Regions.Select(AsTuple).ForEach(MarkRegion);

		private void MarkRegion((Village Village, List<Coordinates> Coordinates) region)
			=> region.Coordinates.Select(AsCells).ForEach(region.Village.AddCellToRegion);

		private (Village Village, List<Coordinates> Coordinates) AsTuple(Village.Data region)
			=> (CreateVillage(_field.Cells[region.Coordinates.Row, region.Coordinates.Column]), region.Cells);

		private Cell AsCells(Coordinates cellCoordinates) => _field.Cells[cellCoordinates.Row, cellCoordinates.Column];

		private Village CreateVillage(Cell cell)
		{
			var village = _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform);
			cell.Building = village;
			return village;
		}
	}
}