using System;
using UnityEngine;

namespace Confrontation
{
	public abstract partial class Building
	{
		[Serializable]
		public class Data
		{
			[SerializeField] private Building _prefab;

			// ReSharper disable once NotAccessedField.Local - usage in BuildingDataPropertyDrawer
			[SerializeField] private int _selectionIndex;

			public Building Prefab
			{
				get => _prefab;
				set => _prefab = value;
			}
		}

		[Serializable]
		public class CoordinatedData : Data
		{
			[SerializeField] private Coordinates _coordinates;

			public Coordinates Coordinates
			{
				get => _coordinates;
				set => _coordinates = value;
			}
		}
	}
}