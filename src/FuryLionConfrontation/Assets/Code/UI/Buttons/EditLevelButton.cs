using Zenject;

namespace Confrontation
{
	public class EditLevelButton : LevelButtonBase
	{
		[Inject] private readonly ToLevelEditor _toLevelEditor;

		protected override ToSceneBase ToScene => _toLevelEditor;
	}
}