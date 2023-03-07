using System.Threading.Tasks;

namespace Confrontation
{
	public class ToGameplay : ToSceneBase
	{
		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override async Task Transfer()
		{
			await Mediator.ShowLoadingCurtain();
			await base.Transfer();
			await Mediator.HideLoadingCurtain();
		}
	}
}