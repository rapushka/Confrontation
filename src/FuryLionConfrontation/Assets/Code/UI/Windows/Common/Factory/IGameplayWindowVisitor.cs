namespace Confrontation
{
	public interface IGameplayWindowVisitor
	{
		GameplayWindowBase Visit(BuildWindow window);
		GameplayWindowBase Visit(BuildingInfoWindow window);
		GameplayWindowBase Visit(GameResultsWindow window);
		GameplayWindowBase Visit(NotEnoughGoldWindow window);
		GameplayWindowBase Visit(SpellBookWindow window);
		GameplayWindowBase Visit(NotEnoughManaWindow window);
	}
}