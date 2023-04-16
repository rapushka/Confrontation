using Zenject;

namespace Confrontation
{
	public class SpellBookButton : ButtonBase
	{
		[Inject] private readonly GameplayWindows _gameplayWindows;

		protected override void OnButtonClick() => _gameplayWindows.Open<SpellBookWindow>();
	}
}