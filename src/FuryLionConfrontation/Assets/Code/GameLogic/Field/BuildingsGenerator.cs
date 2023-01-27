using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings() => _levelSelector.SelectedLevel.Buildings.ForEach(Create);

		private void Create(BuildingData data)
		{
			var fieldCell = _field.Cells[data.Coordinates];
			var ownerId = fieldCell.RelatedRegion.OwnerPlayerId;
			var building = _buildingsFactory.Create(data.Prefab, fieldCell.transform, ownerId);
			fieldCell.Building = building;
		}
	}
}