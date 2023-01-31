using Zenject;

namespace Confrontation
{
	public class Orders
	{
		[Inject] private readonly User _user;

		public void GiveOrder(Cell startCell, Cell endCell)
		{
			if (startCell.HaveUnits
			    && startCell.IsBelongTo(_user.Player)
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