using Zenject;

namespace Confrontation
{
	public class BuildingSpawner
	{
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly IField _field;

		public void Build(Building buildingPrefab, Cell inputClickedCell)
			=> _field.Buildings.Add(_buildingsFactory.Create(buildingPrefab, inputClickedCell));
	}
}