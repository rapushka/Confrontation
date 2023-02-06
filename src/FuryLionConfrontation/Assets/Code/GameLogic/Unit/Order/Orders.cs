using Zenject;

namespace Confrontation
{
	public class Orders
	{
		[Inject] private readonly User _user;

		public void GiveOrder(Cell startCell, Cell endCell)
		{
			if (startCell.HasUnits
			    && startCell.IsBelongTo(_user.Player)
			    && endCell.Building is Village)
			{
				var squad = startCell.LocatedUnits!;
				var quantityToMove = startCell.Building is Barracks
					? squad.QuantityOfUnits
					: squad.QuantityOfUnits / 2;

				squad.MoveTo(endCell, quantityToMove);
			}
		}
	}
}