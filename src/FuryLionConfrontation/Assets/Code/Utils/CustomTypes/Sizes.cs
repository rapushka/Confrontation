using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Sizes
	{
		[field: SerializeField] public int Width  { get; set; }
		[field: SerializeField] public int Height { get; set; }

		public Sizes(int height, int width)
		{
			Height = height;
			Width = width;
		}

		public bool IsInBounds(Coordinates coordinates) => IsInBounds(coordinates.Row, coordinates.Column);

		public bool IsInBounds(int row, int column)
			=> row > 0
			   && row < Width
			   && column > 0
			   && column < Height;
	}
}