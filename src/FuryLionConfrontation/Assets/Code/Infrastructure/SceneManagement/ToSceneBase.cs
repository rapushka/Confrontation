using System.Threading.Tasks;
using Zenject;

namespace Confrontation
{
	public abstract class ToSceneBase
	{
		[Inject] protected readonly ISceneTransferService SceneTransfer;
		[Inject] protected readonly GameUiMediator Mediator;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		protected abstract string SceneName { get; }

		public virtual async Task Transfer()
		{
			if (SceneTransfer.IsCurrentScene(SceneName) == false)
			{
				_routinesRunner.StopAllRoutines();
				await SceneTransfer.ToScene(SceneName);
			}
		}
	}
}