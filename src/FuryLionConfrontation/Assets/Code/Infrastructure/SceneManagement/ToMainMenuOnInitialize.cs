using System.Threading.Tasks;

namespace Confrontation
{
	public class ToMainMenuOnInitialize : ToSceneOnInitialize
	{
		protected override string SceneName => Constants.SceneName.MainMenuScene;

		public override async Task Transfer()
		{
			await Mediator.HideLoadingCurtain();
			await base.Transfer();
		}
	}
}