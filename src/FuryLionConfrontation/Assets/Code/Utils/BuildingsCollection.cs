using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation
{
	public static class BuildingsCollection
	{
		private static readonly List<(string Name, string Path)> _buildings
			= new()
			{
				(nameof(Settlement), Constants.ResourcePath.Settlement),
				(nameof(GoldenMine), Constants.ResourcePath.GoldenMine),
				(nameof(Barrack), Constants.ResourcePath.Barrack),
				(nameof(Capital), Constants.ResourcePath.Capital),
				(nameof(Farm), Constants.ResourcePath.Farm),
				(nameof(Stable), Constants.ResourcePath.Stable),
				(nameof(Forge), Constants.ResourcePath.Forge),
			};

		private static readonly List<(string Name, Building Prefab)> _buildingsPrefabs;

		static BuildingsCollection() 
			=> _buildingsPrefabs = _buildings.Select((b) => (b.Name, Resources.Load<Building>(b.Path))).ToList();

		public static string[] BuildingsNames => _buildings.Select((b) => b.Name).ToArray();

		public static Building Load(string name)
			=> Resources.Load<Building>(_buildings.Single((b) => b.Name == name).Path);

		public static Building Load(int index) => Resources.Load<Building>(_buildings[index].Path);

		public static int IndexOf(Building building) 
			=> _buildingsPrefabs.FindIndex((b) => b.Prefab.GetType() == building.GetType());
	}
}