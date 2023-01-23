using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private RegionColor _color;
		[SerializeField] private MouseClickReceiver _mouseClickReceiver;

		private Coordinates _coordinates;
		private Village _relatedRegion;

		public event Action<Cell> MouseClick;

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

		private void OnEnable()  => _mouseClickReceiver.MouseClick += OnMouseClick;
		private void OnDisable() => _mouseClickReceiver.MouseClick -= OnMouseClick;

		private void OnMouseClick() => MouseClick?.Invoke(this);

		public bool IsBelongTo(int currentPlayerId)
			=> RelatedRegion is not null && RelatedRegion.OwnerPlayerId == currentPlayerId;
	}
}