using Zenject;

namespace Confrontation
{
	public class Orders
	{
		[Inject] private readonly User _user;
		[Inject] private readonly IField _field;

		public void GiveOrder(Cell startCell, Cell endCell)
		{
			if (startCell.HasUnits
			    && startCell.IsBelongTo(_user.Player)
			    && endCell.Building is Settlement
			    && _field.Neighboring.IsNeighbours(startCell.RelatedRegion, endCell.RelatedRegion))
			{
				var squad = startCell.LocatedUnits!;
				squad.MoveTo(endCell);
			}
		}
	}
}