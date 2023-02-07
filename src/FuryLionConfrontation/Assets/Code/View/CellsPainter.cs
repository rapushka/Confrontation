using System.Linq;
using Zenject;

namespace Confrontation
{
	public class CellsPainter : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize() => PaintCells();

		private void PaintCells()
		{
			foreach (var village in _field.Buildings.OfType<Village>())
			{
				foreach (var cell in village.CellsInRegion)
				{
					cell.SetColor(_field.Regions[village.Coordinates].OwnerPlayerId);
				}
			}
		}
	}
}