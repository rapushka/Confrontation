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
			};

		public static string[] BuildingsNames => _buildings.Select((t) => t.Name).ToArray();

		public static Building Load(int index) => Resources.Load<Building>(_buildings[index].Path);
	}
}