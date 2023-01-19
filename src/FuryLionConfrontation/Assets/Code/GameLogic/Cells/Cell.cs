using System;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[field: SerializeField] public Data Value { get; set; }
		[SerializeField] private Building _building;

		public Building Building
		{
			get => _building;
			set
			{
				_building = value;
				Value.BuildingId = 0;
			}
		}

		[Serializable]
		public class Data
		{
			private const int None = -1;

			public Coordinates Coordinates   { get; set; }
			public int         OwnerPlayerId { get; set; } = None;
			public int         BuildingId    { get; set; } = None;

			public bool IsNeutral => OwnerPlayerId == None;

			public bool IsEmpty => BuildingId == None;
		}
	}
}