namespace Confrontation
{
	public abstract class GameplayWindowBase : WindowBase
	{
		public abstract GameplayWindowBase Accept(IGameplayWindowVisitor visitor);
	}
}