using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Regions : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IAssetsService _assets;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _resources.CurrentLevel.Regions.ForEach(ToRegion);

		private void ToRegion(Region region)
		{
			var village = CreateVillage(region);
			GetCellsFrom(region).ForEach(village.AddToRegion);
		}

		private IEnumerable<Cell> GetCellsFrom(Region region) => region.CellsCoordinates.Select((c) => _field.Cells[c]);

		private Village CreateVillage(Region region)
		{
			var ownerCell = _field.Cells[region.VillageCoordinates];
			var village = InstantiateVillage(ownerCell);
			village.OwnerPlayerId = region.OwnerPlayerId;
			ownerCell.Building = village;
			return village;
		}

		private Village InstantiateVillage(Component ownerCell)
			=> _assets.Instantiate(original: _resources.VillagePrefab, parent: ownerCell.transform);
	}
}