using System.Linq;
using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly IField _field;
		[Inject] private readonly IResourcesService _resources;

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings()
		{
			foreach (var building in _levelSelector.SelectedLevel.Buildings.Select(Create))
			{
				_field.Buildings.Add(building);
			}
		}

		private Building Create(Building.Data data)
		{
			var cell = _field.Cells[data.Coordinates];
			var building = _buildingsFactory.Create(data.Prefab, cell);

			if (building is Capital capital)
			{
				capital.Barracks = _buildingsFactory.Create(_resources.Buildings.OfType<Barracks>().Single(), cell);
				capital.GoldenMine = _buildingsFactory.Create(_resources.Buildings.OfType<GoldenMine>().Single(), cell);
				return capital;
			}

			return building;
		}
	}
}