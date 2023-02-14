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
		[Inject] private readonly GameSession _gameSession;

		private Coordinates _coordinates;
		private int _ownerPlayerId;

		private IEnumerable<Cell> CellsInRegion => _field.Cells.Where((c) => c.OwnerPlayerId == OwnerPlayerId);

		public int OwnerPlayerId
		{
			get => _ownerPlayerId;
			set
			{
				var oldOwner = _ownerPlayerId;
				_ownerPlayerId = value;
				UpdateCellsColor();
				UpdateOwnerOfUnitsInRegion();
				CheckIfIsLostCapital(oldOwner);
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

		private void CheckIfIsLostCapital(int oldOwnerId)
		{
			if (RegionWasNeutral(oldOwnerId) == false
			    && PlayerLostAllCapitals(oldOwnerId))
			{
				_gameSession.PlayerLoose(oldOwnerId);
			}
		}

		private static bool RegionWasNeutral(int oldOwnerId) => oldOwnerId == 0;

		private bool PlayerLostAllCapitals(int oldOwnerId) => CapitalsOfPlayer(oldOwnerId).Any() == false;

		private IEnumerable<Capital> CapitalsOfPlayer(int oldOwnerId)
			=> _field.Buildings.OfType<Capital>()
			         .Where((c) => c.OwnerPlayerId == oldOwnerId);

		[Serializable]
		public class Data
		{
			[field: SerializeField] public int OwnerPlayerId { get; set; }

			[field: SerializeField] public List<Coordinates> CellsCoordinates { get; set; } = new();
		}

		public class Factory : PlaceholderFactory<Region> { }
	}
}