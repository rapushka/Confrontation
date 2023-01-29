namespace Confrontation
{
	public class Orders
	{
		public void GiveOrder(Cell startCell, Cell endCell)
		{
			if (startCell.HaveUnits 
			    && endCell.Building is Village)
			{
				startCell.UnitsSquads!.TargetCell = endCell;
			}
		}
	}
}