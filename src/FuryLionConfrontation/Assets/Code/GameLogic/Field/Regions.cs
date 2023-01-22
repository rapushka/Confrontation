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

		private void DivideIntoRegions()
		{
			foreach (var region in _resources.CurrentLevel.Regions)
			{
				var villageCoordinates = region.Coordinates;
				var village = CreateVillage(_field.Cells[villageCoordinates.Row, villageCoordinates.Column]);
				foreach (var cell in region.Cells.Select((cc) => _field.Cells[cc.Row, cc.Column]))
				{
					village.CellsInRegion.Add(cell);
					cell.ToRedRegion();
				}
			}
		}

		private Village CreateVillage(Cell cell)
		{
			var village = _assets.Instantiate(original: _resources.VillagePrefab, parent: cell.transform);
			cell.Building = village;
			return village;
		}
	}
}