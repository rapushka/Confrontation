using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private MaterialByRegion _materialByRegion;

		[field: SerializeField] public Coordinates Coordinates { get; set; }

		[field: SerializeField] public Building Building { get; set; }

		public bool IsEmpty => Building is null;

		public void ToNeutralRegion() => _materialByRegion.ChangeMaterialToNeutral();

		public void ToRedRegion() => _materialByRegion.ChangeMaterialToNeutral();
	}
}