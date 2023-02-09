namespace Confrontation
{
	public class ToMainMenu : ToSceneBase
	{
		protected override string SceneName => Constants.SceneName.MainMenuScene;

		public override void Transfer()
		{
			Mediator.ShowLoadingCurtain();
			base.Transfer();
			Mediator.HideLoadingCurtain();
		}
	}
}