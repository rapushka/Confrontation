using Zenject;

namespace Confrontation
{
	public abstract class ToScene : IInitializable
	{
		[Inject] protected readonly ISceneTransferService SceneTransfer;

		protected abstract string SceneName { get; }

		public virtual void Initialize()
		{
			if (SceneTransfer.IsCurrentScene(SceneName) == false)
			{
				SceneTransfer.ToScene(SceneName);
			}
		}
	}
}