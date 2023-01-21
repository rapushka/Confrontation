using UnityEngine;

namespace Confrontation
{
	public interface IResourceService
	{
		Cell    CellPrefab    { get; }
		Village VillagePrefab { get; }
		Level   CurrentLevel  { get; }
	}

	public class ResourcesService : ScriptableObject, IResourceService
	{
		[field: SerializeField] public Cell    CellPrefab    { get; private set; }
		[field: SerializeField] public Village VillagePrefab { get; private set; }
		[field: SerializeField] public Level   CurrentLevel  { get; private set; }
	}
}