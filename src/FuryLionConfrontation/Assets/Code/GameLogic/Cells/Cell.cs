using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[field: SerializeField] public Coordinates Coordinates { get; set; }

		[field: SerializeField] public Building Building { get; set; }

		public bool IsEmpty => Building is null;
	}
}