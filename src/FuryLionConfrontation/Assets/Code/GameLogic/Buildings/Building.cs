using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class Building : MonoBehaviour, ICoordinated
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IBalanceTable _balanceTable;

		private Coordinates _coordinates;

		public int Level { get; private set; } = 1;

		public abstract string Name { get; }

		public Cell RelatedCell => _field.Cells[Coordinates];

		public bool IsOnMaxLevel => Level >= MaxLevel;

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				_coordinates = value;
				_field.Buildings.Add(this);
			}
		}

		protected IBalanceTable BalanceTable => _balanceTable;

		protected IField Field => _field;

		protected abstract int MaxLevel { get; }

		public void LevelUp()
		{
			if (IsOnMaxLevel == false)
			{
				Level++;
			}
		}

		public override string ToString() => $"{Name} ─ Lvl {Level}";

		[Serializable]
		public class Data
		{
			[SerializeField] private Building _prefab;
			[SerializeField] private Coordinates _coordinates;

			// ReSharper disable once NotAccessedField.Local - usage in BuildingDataPropertyDrawer
			[SerializeField] private int _selectionIndex;

			public Building Prefab
			{
				get => _prefab;
				set => _prefab = value;
			}

			public Coordinates Coordinates
			{
				get => _coordinates;
				set => _coordinates = value;
			}
		}

		public class Factory : PlaceholderFactory<Building, Building>
		{
			public T Create<T>(T prefab, Cell cell)
				where T : Building
			{
				var building = base.Create(prefab);
				building.transform.SetParent(cell.transform, worldPositionStays: false);
				building.Coordinates = cell.Coordinates;
				return (T)building;
			}
			
			public T Create<T>(T prefab)
				where T : Building
			{
				var building = base.Create(prefab);
				return (T)building;
			}
		}
	}
}