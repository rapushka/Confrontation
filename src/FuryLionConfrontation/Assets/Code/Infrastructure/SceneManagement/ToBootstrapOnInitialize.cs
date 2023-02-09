namespace Confrontation
{
	public class ToBootstrapOnInitialize : ToSceneOnInitialize
	{
		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override void Transfer()
		{
			Mediator.ShowImmediatelyLoadingCurtain();
			base.Transfer();
		}
	}
}