using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private MaterialByRegion _materialByRegion;
		[SerializeField] private Coordinates _coordinates;

		
		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				transform.position = value.CalculatePosition().AsTopDown();
				_coordinates = value;
			}
		}

		[field: SerializeField] public Building Building { get; set; }

		public bool IsEmpty => Building is null;

		public void ToNeutralRegion() => _materialByRegion.ChangeMaterialToNeutral();

		public void ToRedRegion() => _materialByRegion.ChangeMaterialToRed();
	}
}