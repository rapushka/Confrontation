using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private MaterialByRegion _materialByRegion;
		[SerializeField] private Coordinates _coordinates;
		[SerializeField] private Village _relatedRegion;

		[field: SerializeField] [CanBeNull] public Building Building { get; set; }

		public Village RelatedRegion
		{
			get => _relatedRegion;
			set
			{
				_materialByRegion.ChangeMaterialTo(value.OwnerPlayerId);
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

		private bool IsBelongTo(int playerId) => RelatedRegion.OwnerPlayerId == playerId;
	}
}