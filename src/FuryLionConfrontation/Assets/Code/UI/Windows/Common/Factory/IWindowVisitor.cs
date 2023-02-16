namespace Confrontation
{
	public interface IWindowVisitor
	{
		WindowBase Visit(BuildWindow window);
		WindowBase Visit(BuildingInfoWindow infoWindow);
		WindowBase Visit(GameResultsWindow window);
	}
}