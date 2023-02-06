using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Village        VillagePrefab { get; }
		Capital        CapitalPrefab { get; }
		List<Building> Buildings     { get; }
	}

	[CreateAssetMenu(fileName = "Resources", menuName = nameof(Confrontation) + "/Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		[field: SerializeField] public Village        VillagePrefab { get; private set; }
		[field: SerializeField] public Capital        CapitalPrefab { get; private set; }
		[field: SerializeField] public List<Building> Buildings     { get; private set; }
	}
}