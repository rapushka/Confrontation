using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Region : ICoordinated
	{
		[Inject] private readonly int _id;
		[Inject] private readonly IField _field;
		[Inject] private readonly ISession _gameSession;

		private Coordinates _coordinates;
		private int _ownerPlayerId;

		public IEnumerable<Cell> OurCells => _field.Cells.Where((c) => c.OwnerPlayerId == OwnerPlayerId);

		public IEnumerable<Cell> CellsInRegion => _field.Cells.Where((c) => c.RelatedRegion == this);

		public int Id => _id;

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
			foreach (var cell in OurCells)
			{
				cell.SetColor(OwnerPlayerId);
			}
		}

		public static bool operator ==(Region left, Region right) => left is not null && left.Equals(right);

		public static bool operator !=(Region left, Region right) => !(left == right);

		public override bool Equals(object other) => other is Region && GetHashCode() == other.GetHashCode();

		public override int GetHashCode() => Id.GetHashCode();

		public void MakeNeutral() => OwnerPlayerId = Constants.NeutralRegion;

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

		private static bool RegionWasNeutral(int oldOwnerId) => oldOwnerId == Constants.NeutralRegion;

		private bool PlayerLostAllCapitals(int oldOwnerId) => CapitalsOfPlayer(oldOwnerId).Any() == false;

		private IEnumerable<Capital> CapitalsOfPlayer(int oldOwnerId)
			=> _field.Buildings.OfType<Capital>()
			         .Where((c) => c.OwnerPlayerId == oldOwnerId);

		[Serializable]
		public class Data
		{
			public Data(int ownerPlayerId, List<Coordinates> cellsCoordinates)
			{
				OwnerPlayerId = ownerPlayerId;
				CellsCoordinates = cellsCoordinates;
			}

			public Data() { }

			[field: SerializeField] public int OwnerPlayerId { get; set; }

			[field: SerializeField] public List<Coordinates> CellsCoordinates { get; set; } = new();
		}

		public class Factory : PlaceholderFactory<int, Region>
		{
			private int _currentId;

			public Region Create() => base.Create(_currentId++);
		}
	}
}