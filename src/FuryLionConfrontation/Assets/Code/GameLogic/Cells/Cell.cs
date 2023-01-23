using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;
		[Header("Game logic data")]
		[SerializeField] private Coordinates _coordinates;
		[SerializeField] private Village _relatedRegion;

		[field: SerializeField] [CanBeNull] public Building Building { get; set; }

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

		public bool IsEmpty => Building is null;
	}
}