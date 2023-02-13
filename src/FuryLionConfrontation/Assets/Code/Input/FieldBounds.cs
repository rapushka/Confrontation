using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class FieldBounds : IInitializable
	{
		[Inject] private readonly IField _field;

		private Bounds _bounds;

		public void Initialize()
		{
			foreach (var position in _field.Cells.Select((c) => c.gameObject.transform.position.FromTopDown()))
			{
				_bounds.UpdateBounds(position);
			}
		}
		
		public bool IsInBounds(Vector2 position, float maxDeviation) => _bounds.IsInBounds(position, maxDeviation);
	}
}