using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Sizes
	{
		[field: SerializeField] public int Width  { get; private set; }
		[field: SerializeField] public int Height { get; private set; }

		public Sizes(int height, int width)
		{
			Height = height;
			Width = width;
		}
	}
}