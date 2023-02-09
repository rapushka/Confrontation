namespace Confrontation
{
	public interface IWindowVisitor
	{
		WindowBase Visit(BuildWindow window);
		WindowBase Visit(BuildingWindow window);
		WindowBase Visit(GameResultsWindow window);
	}
}