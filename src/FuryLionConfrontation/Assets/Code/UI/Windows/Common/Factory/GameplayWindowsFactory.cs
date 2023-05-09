using Zenject;

namespace Confrontation
{
	public class GameplayWindowsFactory
		: IFactory<GameplayWindowBase, GameplayWindowBase>, IGameplayWindowVisitor
	{
		[Inject] private readonly BuildWindow.Factory _buildWindowFactory;
		[Inject] private readonly BuildingInfoWindow.Factory _buildingWindowFactory;
		[Inject] private readonly GameResultsWindow.Factory _gameResultWindowFactory;
		[Inject] private readonly NotEnoughGoldWindow.Factory _notEnoughGoldWindowFactory;
		[Inject] private readonly NotEnoughManaWindow.Factory _notEnoughManaWindowFactory;
		[Inject] private readonly SpellBookWindow.Factory _spellBookWindowFactory;
		[Inject] private readonly TutorialWindow.Factory _tutorialWindowFactory;
		[Inject] private readonly PauseWindow.Factory _pauseWindowFactory;

		private WindowBase _window;

		public GameplayWindowBase Create(GameplayWindowBase windowPrefab) => windowPrefab.Accept(this);

		public GameplayWindowBase Visit(BuildWindow window) => _buildWindowFactory.Create(window);

		public GameplayWindowBase Visit(BuildingInfoWindow window) => _buildingWindowFactory.Create(window);

		public GameplayWindowBase Visit(GameResultsWindow window) => _gameResultWindowFactory.Create(window);

		public GameplayWindowBase Visit(NotEnoughGoldWindow window) => _notEnoughGoldWindowFactory.Create(window);

		public GameplayWindowBase Visit(SpellBookWindow window) => _spellBookWindowFactory.Create(window);

		public GameplayWindowBase Visit(NotEnoughManaWindow window) => _notEnoughManaWindowFactory.Create(window);

		public GameplayWindowBase Visit(TutorialWindow window) => _tutorialWindowFactory.Create(window);

		public GameplayWindowBase Visit(PauseWindow window) => _pauseWindowFactory.Create(window);
	}
}