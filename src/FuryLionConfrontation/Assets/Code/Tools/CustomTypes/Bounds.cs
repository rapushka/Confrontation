using UnityEngine;

namespace Confrontation
{
	public struct Bounds
	{
		private Vector2 _minBorder;
		private Vector2 _maxBorder;

		public void UpdateBounds(Vector2 position)
		{
			_maxBorder.x = Mathf.Max(_maxBorder.x, position.x);
			_maxBorder.y = Mathf.Max(_maxBorder.y, position.y);

			_minBorder.x = Mathf.Min(_minBorder.x, position.x);
			_minBorder.y = Mathf.Min(_minBorder.y, position.y);
		}

		public bool IsInBounds(Vector2 position, float maxDeviation) => Distance(position) < maxDeviation;

		private float Distance(Vector2 position) => Vector2.Distance(position, Clamp(position));

		private Vector2 Clamp(Vector2 position) => position.Clamp(_minBorder, _maxBorder);
	}
}