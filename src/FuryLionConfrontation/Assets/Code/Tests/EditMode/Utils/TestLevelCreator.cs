using System.Collections.Generic;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public class TestLevelCreator : ILevelSelector
	{
		public TestLevelCreator() => SelectedLevel = Create();

		public ILevel SelectedLevel { get; }

		private static ILevel Create()
			=> new Level
			{
				PlayersCount = 2,
				Sizes = new Sizes(5, 5),
				Regions = new List<Region.Data>
				{
					new()
					{
						OwnerPlayerId = 1,
						CellsCoordinates = new List<Coordinates>
						{
							new(row: 1, column: 1),
							new(row: 2, column: 1),
						},
					},
					new()
					{
						OwnerPlayerId = 2,
						CellsCoordinates = new List<Coordinates>
						{
							new(row: 1, column: 2),
						},
					},
				},
				Buildings = new List<Building.Data>
				{
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.GoldenMine),
						Coordinates = new Coordinates(0, 1),
					},
					new()
					{
						Prefab = Resources.Load<Village>(Constants.ResourcePath.Village),
						Coordinates = new Coordinates(1, 1),
					},
				},
			};
	}
}