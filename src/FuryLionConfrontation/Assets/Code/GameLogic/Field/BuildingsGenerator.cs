using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly Field _field;

		public List<Building> Buildings { get; } = new();

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings() => Buildings.AddRange(_levelSelector.SelectedLevel.Buildings.Select(Create));

		private Building Create(BuildingData data)
			=> _buildingsFactory.Create(data.Prefab, _field.Cells[data.Coordinates]);
	}
}