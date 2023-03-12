using Zenject;

namespace Confrontation
{
	public class EditLevelButton : LevelSelectionButtonBase
	{
		[Inject] private readonly ToLevelEditor _toLevelEditor;

		protected override ToSceneBase ToScene => _toLevelEditor;
	}
}