using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class CustomBuildingFactory : IFactory<Building, Transform, int, Building>
	{
		[Inject] private readonly IAssetsService _assets;

		public Building Create(Building prefab, Transform ownerCell, int ownerPlayerId)
		{
			var building = _assets.Instantiate(prefab, parent: ownerCell);
			building.OwnerPlayerId = ownerPlayerId;
			return building;
		}
	}
}