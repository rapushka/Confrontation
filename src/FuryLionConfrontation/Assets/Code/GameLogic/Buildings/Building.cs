using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Confrontation
{
	public abstract partial class Building : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IStatsTable _statsTable;

		[field: SerializeField] public Invisibility Invisibility { get; private set; }

		private Coordinates _coordinates;

		protected int Level { get; private set; } = 1;

		public abstract string Name { get; }

		public Cell RelatedCell => _field.Cells[Coordinates];

		public bool IsOnMaxLevel => Level >= MaxLevel;

		public virtual int OwnerPlayerId
		{
			get => _field.Regions[Coordinates].OwnerPlayerId;
			set => _field.Regions[Coordinates].OwnerPlayerId = value;
		}

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Buildings.Add(this);
			}
		}

		protected IStatsTable StatsTable => _statsTable;

		protected IField Field => _field;

		public abstract int UpgradePrice { get; }

		protected abstract int MaxLevel { get; }

		public virtual void LevelUp()
		{
			if (IsOnMaxLevel == false)
			{
				Level++;
			}
		}

		public override string ToString() => $"{Name} ─ Lvl {Level}";

		public class Factory : PlaceholderFactory<Building, Building>
		{
			public T Create<T>(T prefab, Cell cell)
				where T : Building
			{
				Assert.IsNotNull(prefab);
				var building = base.Create(prefab);
				building.transform.SetParent(cell.transform, worldPositionStays: false);
				building.Coordinates = cell.Coordinates;
				return (T)building;
			}
		}
	}
}