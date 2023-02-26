using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class LevelCreator : ILevelSelector
	{
		public LevelCreator() => SelectedLevel = Create();

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
							new(row: 2, column: 1),
							new(row: 1, column: 1),
							new(row: 0, column: 1),
							new(row: 2, column: 2),
							new(row: 1, column: 2),
							new(row: 0, column: 2),
							new(row: 1, column: 0),
						},
					},
					new()
					{
						OwnerPlayerId = 2,
						CellsCoordinates = new List<Coordinates>
						{
							new(row: 4, column: 3),
							new(row: 3, column: 3),
							new(row: 2, column: 3),
							new(row: 4, column: 4),
							new(row: 3, column: 4),
							new(row: 2, column: 4),
							new(row: 3, column: 2),
						},
					},
				},
				Buildings = new List<Building.CoordinatedData>
				{
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.GoldenMine),
						Coordinates = new Coordinates(0, 1),
					},
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.Barrack),
						Coordinates = new Coordinates(0, 2),
					},
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.Barrack),
						Coordinates = new Coordinates(4, 3),
					},
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.Village),
						Coordinates = new Coordinates(1, 1),
					},
					new()
					{
						Prefab = Resources.Load<Building>(Constants.ResourcePath.Village),
						Coordinates = new Coordinates(3, 3),
					},
				},
			};
	}
}