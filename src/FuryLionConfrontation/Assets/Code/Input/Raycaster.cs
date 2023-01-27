using UnityEngine;

namespace Confrontation
{
	public class Raycaster
	{
		private Camera _camera;

		private Camera Camera => _camera ??= Camera.main;

		public void DetectObject(Vector2 clickPosition)
		{
			var ray = Camera.ScreenPointToRay(clickPosition);
			if (Physics.Raycast(ray, out var hit)
			    && hit.collider != null)
			{
				Debug.Log($"hit: {hit.collider.name} - {hit.transform.position}");
			}
		}
	}
}