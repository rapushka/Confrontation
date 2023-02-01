using Zenject;

namespace Confrontation
{
	public abstract class ToScene : IInitializable
	{
		[Inject] private readonly ISceneTransferService _sceneTransfer;

		protected abstract string SceneName { get; }

		public virtual void Initialize()
		{
			if (_sceneTransfer.IsCurrentScene(SceneName) == false)
			{
				_sceneTransfer.ToScene(SceneName);
			}
		}
	}
}