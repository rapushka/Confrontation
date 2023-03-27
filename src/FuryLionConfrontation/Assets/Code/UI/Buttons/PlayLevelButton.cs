using Zenject;

namespace Confrontation
{
	public class PlayLevelButton : LevelButtonBase
	{
		[Inject] private readonly ToGameplay _toGameplay;

		protected override ToSceneBase ToScene => _toGameplay;
	}
}