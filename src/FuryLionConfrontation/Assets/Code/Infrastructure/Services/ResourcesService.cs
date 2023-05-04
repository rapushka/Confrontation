using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Settlement SettlementPrefab { get; }

		Capital CapitalPrefab { get; }

		TypedDictionary<Building> Buildings { get; }
	}

	[CreateAssetMenu(fileName = "Resources", menuName = nameof(Confrontation) + "/Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		private TypedDictionary<Building> _buildingsDictionary;

		[field: SerializeField] private List<Building> _buildings;

		[field: SerializeField] public Settlement SettlementPrefab { get; private set; }
		[field: SerializeField] public Capital    CapitalPrefab    { get; private set; }

		public TypedDictionary<Building> Buildings
			=> _buildingsDictionary ??= new TypedDictionary<Building>(_buildings);
	}
}