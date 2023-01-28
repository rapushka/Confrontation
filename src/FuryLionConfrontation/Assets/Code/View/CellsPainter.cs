using Zenject;

namespace Confrontation
{
	public class CellsPainter : IInitializable
	{
		[Inject] private readonly RegionsGenerator _regions;

		public void Initialize() => PaintCells();

		private void PaintCells()
		{
			foreach (var village in _regions.Villages)
			{
				foreach (var cell in village.CellsInRegion)
				{
					cell.SetColor(village.OwnerPlayerId);
				}
			}
		}
	}
}