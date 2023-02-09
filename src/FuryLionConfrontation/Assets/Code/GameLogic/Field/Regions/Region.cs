using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Region : ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly PlayersList _playersList;

		private Coordinates _coordinates;
		private int _ownerPlayerId;

		private IEnumerable<Cell> CellsInRegion => _field.Cells.Where((c) => c.OwnerPlayerId == OwnerPlayerId);

		public int OwnerPlayerId
		{
			get => _ownerPlayerId;
			set
			{
				CheckIfIsLostCapital();
				_ownerPlayerId = value;
				UpdateCellsColor();
				UpdateOwnerOfUnitsInRegion();
			}
		}

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Regions.Add(this);
			}
		}

		public void UpdateCellsColor()
		{
			foreach (var cell in CellsInRegion)
			{
				cell.SetColor(OwnerPlayerId);
			}
		}

		private void UpdateOwnerOfUnitsInRegion()
		{
			foreach (var cellInRegion in _field.Cells.Where((c) => c.RelatedRegion == this))
			{
				if (cellInRegion.LocatedUnits is not null)
				{
					cellInRegion.LocatedUnits!.OwnerPlayerId = OwnerPlayerId;
				}
			}
		}

		private void CheckIfIsLostCapital()
		{
			var buildingsByPlayer = _field.Buildings.OfType<Capital>()
			                              .Where((c) => c.OwnerPlayerId == OwnerPlayerId)
			                              .ToArray();

			if (buildingsByPlayer.Length == 1)
			{
				var id = buildingsByPlayer.Single().OwnerPlayerId;
				_playersList.Players.Single((p) => p.Id == id).Loose();
			}
		}

		[Serializable]
		public class Data
		{
			[field: SerializeField] public int               OwnerPlayerId    { get; set; }
			[field: SerializeField] public List<Coordinates> CellsCoordinates { get; set; } = new();
		}

		public class Factory : PlaceholderFactory<Region> { }
	}
}