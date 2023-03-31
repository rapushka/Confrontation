using System.Threading.Tasks;

namespace Confrontation
{
	public class ToLevelEditor : ToSceneBase
	{
		protected override string SceneName => Constants.SceneName.LevelEditorScene;

		public override async Task Transfer()
		{
			await Mediator.ShowLoadingCurtain();
			await base.Transfer();
			await Mediator.HideLoadingCurtain();
		}
	}
}