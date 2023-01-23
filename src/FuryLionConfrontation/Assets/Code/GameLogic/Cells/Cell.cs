using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;

		private Coordinates _coordinates;
		private Village _relatedRegion;

		[CanBeNull] public Building Building { get; set; }

		public bool IsEmpty => Building is null;

		public Village RelatedRegion
		{
			get => _relatedRegion;
			set
			{
				_color.ChangeMaterialTo(value.OwnerPlayerId);
				_relatedRegion = value;
			}
		}

		public Coordinates Coordinates
		{
			get => _coordinates;
			set
			{
				transform.position = value.CalculatePosition().AsTopDown();
				_coordinates = value;
			}
		}
	}
}