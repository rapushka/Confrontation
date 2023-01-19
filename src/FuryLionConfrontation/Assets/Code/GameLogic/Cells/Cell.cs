using System;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[field: SerializeField] public CellData Data { get; set; }

		public bool IsNeutral => Data.IsNeutral;

		public bool IsEmpty => Data.IsEmpty;
	}

	[Serializable]
	public class CellData
	{
		public Coordinates Coordinates;
		public int OwnerPlayerId = -1;
		public int BuildingId = -1;
		
		public bool IsNeutral => OwnerPlayerId == -1;

		public bool IsEmpty => BuildingId == -1;
	}
}