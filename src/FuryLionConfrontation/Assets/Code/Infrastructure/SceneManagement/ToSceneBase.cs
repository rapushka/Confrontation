using Zenject;

namespace Confrontation
{
	public abstract class ToSceneBase
	{
		[Inject] protected readonly ISceneTransferService SceneTransfer;
		[Inject] protected readonly GameUiMediator Mediator;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		protected abstract string SceneName { get; }

		public virtual void Transfer()
		{
			if (SceneTransfer.IsCurrentScene(SceneName) == false)
			{
				_routinesRunner.StopAllRoutines();
				SceneTransfer.ToScene(SceneName);
			}
		}
	}
}