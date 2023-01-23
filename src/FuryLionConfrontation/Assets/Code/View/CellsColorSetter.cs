using Zenject;

namespace Confrontation
{
	public class CellsColorSetter : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly IResourcesService _resources;

		public void Initialize() => MarkCells();

		private void MarkCells()
		{
			_field.Cells.ForEach(ToNeutral);
			_resources.CurrentLevel.Regions.ForEach(ToRegion);
		}

		private static void ToNeutral(Cell cell) { }

		private void ToRegion(Region region) { }
	}
}