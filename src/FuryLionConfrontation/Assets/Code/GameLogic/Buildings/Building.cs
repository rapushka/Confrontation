using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;

		private Coordinates _coordinates;

		public int OwnerPlayerId { get; set; }

		public Cell RelatedCell => _field.Cells[Coordinates];

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				BuildingsStorage.Buildings.Add(this);
			}
		}

		public class Factory : PlaceholderFactory<Building, Building>
		{
			public T Create<T>(T prefab, Cell ownerCell, int ownerId)
				where T : Building
			{
				var building = Create(prefab, ownerCell.transform, ownerId);
				building.Coordinates = ownerCell.Coordinates;
				return building;
			}

			public T Create<T>(T prefab, Transform ownerCell, int ownerId)
				where T : Building
			{
				var building = base.Create(prefab);
				building.transform.SetParent(ownerCell, worldPositionStays: false);
				building.OwnerPlayerId = ownerId;
				return (T)building;
			}

			public Building Create(Building prefab, Cell cell)
			{
				var building = Create(prefab, cell.transform, cell.RelatedRegion.OwnerPlayerId);
				building.Coordinates = cell.Coordinates;
				return building;
			}

			public T Create<T>(Building prefab)
				where T : Building
				=> (T)base.Create(prefab);
		}
	}
}