using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly Field _field;

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings() => _levelSelector.SelectedLevel.Buildings.ForEach(Create);

		private void Create(BuildingData data) => _buildingsFactory.Create(data.Prefab, _field.Cells[data.Coordinates]);
	}
}