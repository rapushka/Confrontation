using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[field: SerializeField] public Coordinates Coordinates   { get; set; }
		[field: SerializeField] public int         OwnerPlayerId { get; set; } = None;
		[field: SerializeField] public Building    Building      { get; set; }

		private const int None = -1;

		public bool IsNeutral => OwnerPlayerId == None;

		public bool IsEmpty => Building is null;
	}
}