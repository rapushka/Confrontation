using Zenject;

namespace Confrontation
{
	public class RegionsGenerator : IInitializable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Region.Factory _regionsFactory;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _levelSelector.SelectedLevel.Regions.ForEach(ToRegion);

		private void ToRegion(Region.Data data)
		{
			var region = _regionsFactory.Create();
			region.OwnerPlayerId = data.OwnerPlayerId;

			foreach (var coordinates in data.CellsCoordinates)
			{
				_field.Regions[coordinates] = region;
			}

			region.UpdateCellsColor();
		}
	}
}