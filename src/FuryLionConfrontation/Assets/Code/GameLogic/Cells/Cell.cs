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
			[field: SerializeField] public Coordinates Coordinates   { get; set; }
			[field: SerializeField] public int         OwnerPlayerId { get; set; } = None;
			[field: SerializeField] public int         BuildingId    { get; set; } = None;

			private const int None = -1;

			public bool IsNeutral => OwnerPlayerId == None;

			public bool IsEmpty => BuildingId == None;
		}
	}
}