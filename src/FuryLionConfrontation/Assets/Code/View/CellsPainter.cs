using System.Linq;
using Zenject;

namespace Confrontation
{
	public class CellsPainter : IInitializable, ITickable
	{
		[Inject] private readonly IField _field;

		public void Initialize() { }

		public void Tick() { }

	}
}