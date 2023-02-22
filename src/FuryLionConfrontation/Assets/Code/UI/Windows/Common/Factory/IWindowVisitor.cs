namespace Confrontation
{
	public interface IWindowVisitor
	{
		WindowBase Visit(BuildWindow window);
		WindowBase Visit(BuildingInfoWindow window);
		WindowBase Visit(GameResultsWindow window);
		WindowBase Visit(NotEnoughGoldWindow window);
	}
}