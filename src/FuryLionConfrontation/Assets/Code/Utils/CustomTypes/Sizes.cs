using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Sizes
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;

		public int Width
		{
			get => _width;
			set => _width = value;
		}

		public int Height
		{
			get => _height;
			set => _height = value;
		}

		public Sizes(int height, int width)
		{
			_height = height;
			_width = width;
		}
	}
}