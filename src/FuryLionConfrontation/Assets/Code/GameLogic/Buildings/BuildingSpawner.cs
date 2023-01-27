using Zenject;

namespace Confrontation
{
	public class BuildingSpawner
	{
		[Inject] private readonly User _user;
		[Inject] private readonly Building.Factory _buildingsFactory;

		public void Build(Building building) => _buildingsFactory.Create(building, _user.Player.ClickedCell);
	}
}