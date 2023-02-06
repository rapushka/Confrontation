using System.Collections.Generic;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public class TestLevelCreator : ILevelSelector
	{
		public TestLevelCreator() => SelectedLevel = Create();

		public Level SelectedLevel { get; }

		private static Level Create()
			=> new()
			{
				PlayersCount = 2,
				Sizes = new Sizes(5, 5),
				Regions = new List<RegionData>
				{
					new()
					{
						OwnerPlayerId = 1,
						VillageCoordinates = new Coordinates(1, 1),
						CellsCoordinates = new List<Coordinates>
						{
							new(row: 2, column: 1),
						},
					},
				},
				Buildings = new List<BuildingData>
				{
					new()
					{
						Prefab = Resources.Load<Building>("Prefabs/Golden Mine"),
						Coordinates = new Coordinates(0, 1),
					},
				},
			};
	}
}