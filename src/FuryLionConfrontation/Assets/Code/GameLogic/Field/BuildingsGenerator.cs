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

		private Building Create(Building.CoordinatedData coordinatedData)
		{
			var cell = _field.Cells[coordinatedData.Coordinates];
			var building = _buildingsFactory.Create(coordinatedData.Prefab, cell);

			if (building is Capital capital)
			{
				var barracks = _buildingsFactory.Create(_resources.Barrack, cell);
				var goldenMine = _buildingsFactory.Create(_resources.GoldenMine, cell);

				capital.SetStashedBuildings(barracks, goldenMine);
				return capital;
			}

			return building;
		}
	}
}