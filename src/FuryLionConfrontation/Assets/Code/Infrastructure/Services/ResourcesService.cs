using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Cell          CellPrefab    { get; }
		Village       VillagePrefab { get; }
		RectTransform Canvas        { get; }
	}

	[CreateAssetMenu(fileName = "Resources", menuName = nameof(Confrontation) + "/Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		[field: SerializeField] public Cell          CellPrefab    { get; private set; }
		[field: SerializeField] public Village       VillagePrefab { get; private set; }
		[field: SerializeField] public RectTransform Canvas        { get; private set; }
	}
}