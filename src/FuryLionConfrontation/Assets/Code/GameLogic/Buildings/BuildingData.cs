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
				get => _prefab == true ? _prefab : BuildingsCollection.Load(_selectionIndex);

				set => _prefab = value;
			}
		}

		[Serializable]
		public class CoordinatedData
		{
			[SerializeField] private Data _data = new();
			[SerializeField] private Coordinates _coordinates;

			public Coordinates Coordinates
			{
				get => _coordinates;
				set => _coordinates = value;
			}

			public Building Prefab
			{
				get => _data.Prefab;
				set => _data.Prefab = value;
			}
		}
	}
}