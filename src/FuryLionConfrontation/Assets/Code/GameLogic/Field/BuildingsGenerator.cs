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

		private Barrack Barrack => _resources.Buildings.OfType<Barrack>().Single();

		private GoldenMine GoldenMine => _resources.Buildings.OfType<GoldenMine>().Single();

		private Farm Farm => _resources.Buildings.OfType<Farm>().Single();

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings()
		{
			foreach (var building in _levelSelector.SelectedLevel.Buildings.Select(Create))
			{
				_field.Buildings.Add(building);
			}
		}

		private Building Create(Building.CoordinatedData coordinatedData)
		{
			var cell = _field.Cells[coordinatedData.Coordinates];
			var building = _buildingsFactory.Create(coordinatedData.Prefab, cell);

			if (building is Capital capital)
			{
				var barrack = _buildingsFactory.Create(Barrack, cell);
				var goldenMine = _buildingsFactory.Create(GoldenMine, cell);
				var farm = _buildingsFactory.Create(Farm, cell);

				capital.SetStashedBuildings(barrack, goldenMine, farm);
				return capital;
			}

			return building;
		}
	}
}