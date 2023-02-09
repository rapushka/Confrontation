namespace Confrontation
{
	public class ToMainMenuOnInitialize : ToSceneOnInitialize
	{
		protected override string SceneName => Constants.SceneName.MainMenuScene;

		public override void Transfer()
		{
			Mediator.HideLoadingCurtain();
			base.Transfer();
		}
	}
}