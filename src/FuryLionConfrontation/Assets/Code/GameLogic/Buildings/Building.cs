using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;

		private Coordinates _coordinates;

		public Cell RelatedCell => _field.Cells[Coordinates];

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Buildings.Add(this);
			}
		}

		protected IField Field => _field;

		public class Factory : PlaceholderFactory<Building, Building>
		{
			public T Create<T>(T prefab, Cell ownerCell)
				where T : Building
			{
				var building = Create(prefab, ownerCell.transform);
				building.Coordinates = ownerCell.Coordinates;
				return building;
			}

			public T Create<T>(T prefab, Transform ownerCell)
				where T : Building
			{
				var building = base.Create(prefab);
				building.transform.SetParent(ownerCell, worldPositionStays: false);
				return (T)building;
			}

			public Building Create(Building prefab, Cell cell)
			{
				var building = Create(prefab, cell.transform);
				building.Coordinates = cell.Coordinates;
				return building;
			}
		}
	}
}