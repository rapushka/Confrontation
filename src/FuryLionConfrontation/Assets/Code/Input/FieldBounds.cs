using System.Linq;
using Zenject;

namespace Confrontation
{
	public class FieldBounds : IInitializable
	{
		[Inject] private readonly IField _field;

		private Bounds _bounds;

		public Bounds Bounds => _bounds;

		public void Initialize()
		{
			foreach (var position in _field.Cells.Select((c) => c.gameObject.transform.position))
			{
				_bounds.UpdateBounds(position.FromTopDown());
			}
		}
	}
}