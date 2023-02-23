using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	public interface IResourcesService
	{
		Village        VillagePrefab { get; }
		Capital        CapitalPrefab { get; }
		List<Building> Buildings     { get; }
		Barracks       Barracks      { get; }
		GoldenMine     GoldenMine    { get; }
	}

	[CreateAssetMenu(fileName = "Resources", menuName = nameof(Confrontation) + "/Resources")]
	public class ResourcesService : ScriptableObject, IResourcesService
	{
		private Barracks _barracks;
		private GoldenMine _goldenMine;

		[field: SerializeField] public Village        VillagePrefab { get; private set; }
		[field: SerializeField] public Capital        CapitalPrefab { get; private set; }
		[field: SerializeField] public List<Building> Buildings     { get; private set; }

		public Barracks Barracks => _barracks ??= Buildings.OfType<Barracks>().Single();

		public GoldenMine GoldenMine => _goldenMine ??= Buildings.OfType<GoldenMine>().Single();
	}
}