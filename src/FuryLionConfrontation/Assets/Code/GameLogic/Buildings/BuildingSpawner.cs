using Zenject;

namespace Confrontation
{
	public class BuildingSpawner
	{
		[Inject] private readonly User _user;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly BuildingsGenerator _buildings;

		public void Build(Building buildingPrefab)
			=> _buildings.Buildings.Add(_buildingsFactory.Create(buildingPrefab, _user.Player.ClickedCell));
	}
}