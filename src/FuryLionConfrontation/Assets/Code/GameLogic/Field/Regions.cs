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

		private (Village Village, List<Coordinates> Coordinates) AsTuple(Region region)
			=> (CreateVillage(region), region.Cells);

		private void MarkRegion((Village Village, List<Coordinates> Coordinates) region)
			=> region.Coordinates.Select((c) => _field.Cells[c]).ForEach(region.Village.AddCellToRegion);

		private Village CreateVillage(Region region)
		{
			var cell = _field.Cells[region.Coordinates];
			return _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform)
			              .With((v) => v.OwnerPlayerId = region.OwnerPlayerId)
			              .With((v) => cell.Building = v);
		}
	}
}