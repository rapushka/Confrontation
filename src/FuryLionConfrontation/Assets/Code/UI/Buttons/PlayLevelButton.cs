using Zenject;

namespace Confrontation
{
	public class PlayLevelButton : LevelSelectionButtonBase
	{
		[Inject] private readonly ToGameplay _toGameplay;

		protected override ToSceneBase ToScene => _toGameplay;
	}
}