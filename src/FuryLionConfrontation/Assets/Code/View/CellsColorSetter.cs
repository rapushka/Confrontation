using System.Linq;
using Zenject;

namespace Confrontation
{
	public class CellsColorSetter : IInitializable
	{
		private readonly Field _field;
		private readonly IResourcesService _resources;

		[Inject]
		public CellsColorSetter(Field field, IResourcesService resources)
		{
			_field = field;
			_resources = resources;
		}

		public void Initialize() => MarkCells();

		private void MarkCells()
		{
			_field.Cells.ForEach(ToNeutral);
			_resources.CurrentLevel.Regions.ForEach(ToRegion);
		}

		private static void ToNeutral(Cell cell) => cell.ToRegion(Constants.NeutralRegion);

		private void ToRegion(Region region)
			=> region.Cells.Select((cc) => _field.Cells[cc]).ForEach((c) => c.ToRegion(region.OwnerPlayerId));
	}
}