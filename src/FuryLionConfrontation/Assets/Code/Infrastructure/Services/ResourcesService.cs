using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Cell    CellPrefab    { get; }
		Village VillagePrefab { get; }
	}

	[CreateAssetMenu(menuName = "Confrontation/Resources", fileName = "Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		[field: SerializeField] public Cell    CellPrefab    { get; private set; }
		[field: SerializeField] public Village VillagePrefab { get; private set; }
	}
}