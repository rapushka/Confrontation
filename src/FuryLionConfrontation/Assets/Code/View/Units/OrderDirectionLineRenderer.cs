using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OrderDirectionLineRenderer
	{
		[Inject] private readonly LineRenderer _lineRenderer;

		public void OnOrderStart(Vector3 position) => _lineRenderer.AddPosition(position);

		public void OnOrderEnd() => _lineRenderer.ClearPositions();
	}
}