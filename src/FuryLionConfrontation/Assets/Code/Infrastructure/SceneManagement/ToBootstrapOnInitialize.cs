using System.Threading.Tasks;

namespace Confrontation
{
	public class ToBootstrapOnInitialize : ToSceneOnInitialize
	{
		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override async Task Transfer()
		{
			Mediator.ShowImmediatelyLoadingCurtain();
			await base.Transfer();
		}
	}
}