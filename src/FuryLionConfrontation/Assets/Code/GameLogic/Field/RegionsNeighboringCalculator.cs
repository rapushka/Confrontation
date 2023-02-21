using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class RegionsNeighboringCalculator : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize()
		{
			_field.Neighboring.Neighbouring = _field.Regions.Distinct().ToDictionary((r) => r, GroupByNeighbouring);

			Debug.Log("==========================================================");

			foreach (var (region, neighbors) in _field.Neighboring.Neighbouring)
			{
				Debug.Log($"region = {region.OwnerPlayerId}");

				foreach (var neighbor in neighbors)
				{
					Debug.Log($"\tneighbor to {neighbor.OwnerPlayerId}");
				}
			}
		}

		private IEnumerable<Region> GroupByNeighbouring(Region region)
			=> region.CellsInRegion.SelectMany(CollectNeighboursFor).Distinct();

		private IEnumerable<Region> CollectNeighboursFor(Cell cell)
			=> CollectNeighbors(cell, cell.Coordinates.Row.IsEven() ? IsDiagonallyNext : IsDiagonallyPrevious);

		private IEnumerable<Region> CollectNeighbors(ICoordinated cell, Func<Coordinates, Coordinates, bool> isTooFar)
		{
			var centerRow = cell.Coordinates.Row;
			var centerColumn = cell.Coordinates.Column;

			for (var row = centerRow - 1; row <= centerRow + 1; row++)
			{
				for (var column = centerColumn - 1; column <= centerColumn + 1; column++)
				{
					var currentCoordinates = new Coordinates(row, column);

					if (_field.Cells.Sizes.IsInBounds(currentCoordinates)
					    && isTooFar(cell.Coordinates, currentCoordinates) == false)
					{
						yield return _field.Cells[currentCoordinates].RelatedRegion;
					}
				}
			}
		}

		private static bool IsDiagonallyNext(Coordinates center, Coordinates other)
			=> center.Column + 1 == other.Column
			   && center.Row != other.Row;

		private static bool IsDiagonallyPrevious(Coordinates center, Coordinates other)
			=> center.Column - 1 == other.Column
			   && center.Row != other.Row;
	}
}