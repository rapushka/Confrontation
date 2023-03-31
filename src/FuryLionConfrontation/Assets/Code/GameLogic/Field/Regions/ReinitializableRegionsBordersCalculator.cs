namespace Confrontation
{
	public class ReinitializableRegionsBordersCalculator : RegionsBordersCalculator
	{
		public void Reinitialize()
		{
			Cleanup();
			Initialize();
		}

		private void Cleanup() => Field.Cells.ForEach((c) => c.Borders.HideAll());
	}
}