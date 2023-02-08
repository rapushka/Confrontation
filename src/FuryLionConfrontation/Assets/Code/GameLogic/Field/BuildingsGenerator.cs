using System.Linq;
using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly IField _field;

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings()
		{
			foreach (var building in _levelSelector.SelectedLevel.Buildings.Select(Create))
			{
				_field.Buildings.Add(building);
			}
		}

		private Building Create(Building.Data data)
			=> _buildingsFactory.Create(data.Prefab, _field.Cells[data.Coordinates]);
	}
}