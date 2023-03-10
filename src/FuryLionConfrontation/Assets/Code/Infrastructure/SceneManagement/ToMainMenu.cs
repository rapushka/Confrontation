using System.Threading.Tasks;

namespace Confrontation
{
	public class ToMainMenu : ToSceneBase
	{
		protected override string SceneName => Constants.SceneName.MainMenuScene;

		public override async Task Transfer()
		{
			await Mediator.ShowLoadingCurtain();
			await base.Transfer();
			await Mediator.HideLoadingCurtain();
		}
	}
}