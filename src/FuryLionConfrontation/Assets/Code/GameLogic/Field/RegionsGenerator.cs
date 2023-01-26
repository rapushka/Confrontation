using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionsGenerator : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly IResourcesService _resourcesService;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;

		private Village VillagePrefab => _resourcesService.VillagePrefab;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _levelSelector.SelectedLevel.Regions.ForEach(ToRegion);

		private void ToRegion(Region region)
		{
			var village = CreateVillage(region);
			GetCellsFrom(region).ForEach(village.AddToRegion);
		}

		private IEnumerable<Cell> GetCellsFrom(Region region) => region.CellsCoordinates.Select((c) => _field.Cells[c]);

		private Village CreateVillage(Region region)
		{
			var ownerCell = _field.Cells[region.VillageCoordinates];
			var village = Create(region, ownerCell);
			ownerCell.Building = village;
			return village;
		}

		private Village Create(Region region, Component ownerCell)
			=> _buildingsFactory.Create<Village>(VillagePrefab, ownerCell.transform, region.OwnerPlayerId);
	}
}