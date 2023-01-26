using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class CustomBuildingFactory : IFactory<Component, int, Building>
	{
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IAssetsService _assets;

		public Building Create(Component ownerCell, int ownerPlayerId)
		{
			var village = Instantiate(ownerCell);
			village.OwnerPlayerId = ownerPlayerId;
			return village;
		}
		
		private Village Instantiate(Component ownerCell)
			=> _assets.Instantiate(original: _resources.VillagePrefab, parent: ownerCell.transform);
	}
}