using Zenject;

namespace Confrontation
{
	public class BuildingSpawner
	{
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly IField _field;
		[Inject] private readonly IInputService _input;

		public void Build(Building buildingPrefab)
			=> _field.Buildings.Add(_buildingsFactory.Create(buildingPrefab, _input.ClickedCell));
	}
}