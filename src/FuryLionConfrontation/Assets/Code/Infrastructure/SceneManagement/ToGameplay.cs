namespace Confrontation
{
	public class ToGameplay : ToSceneBase
	{
		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override void Transfer()
		{
			Mediator.ShowLoadingCurtain();
			base.Transfer();
			Mediator.HideLoadingCurtain();
		}
	}
}