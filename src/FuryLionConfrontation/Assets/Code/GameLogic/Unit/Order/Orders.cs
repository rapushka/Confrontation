namespace Confrontation
{
	public class Orders
	{
		public void GiveOrder(Cell startCell, Cell endCell)
		{
			if (startCell.HaveUnits
			    && endCell.Building is Village)
			{
				var squad = startCell.UnitsSquads!;
				var quantityToMove = startCell.Building is Barracks
					? squad.QuantityOfUnits
					: squad.QuantityOfUnits / 2;

				squad.MoveTo(endCell, quantityToMove);
			}
		}
	}
}