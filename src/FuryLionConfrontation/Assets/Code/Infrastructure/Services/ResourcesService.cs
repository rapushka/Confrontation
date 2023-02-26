using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Village VillagePrefab { get; }

		Capital CapitalPrefab { get; }

		TypedDictionary<Building> Buildings { get; }

		Barrack Barrack { get; }

		GoldenMine GoldenMine { get; }
	}

	[CreateAssetMenu(fileName = "Resources", menuName = nameof(Confrontation) + "/Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		private TypedDictionary<Building> _buildingsDictionary;

		[field: SerializeField] private List<Building> _buildings;

		[field: SerializeField] public Village VillagePrefab { get; private set; }
		[field: SerializeField] public Capital CapitalPrefab { get; private set; }

		public TypedDictionary<Building> Buildings => _buildingsDictionary ?? new TypedDictionary<Building>(_buildings);

		public Barrack Barrack => _buildings.OfType<Barrack>().Single();

		public GoldenMine GoldenMine => _buildings.OfType<GoldenMine>().Single();
	}
}