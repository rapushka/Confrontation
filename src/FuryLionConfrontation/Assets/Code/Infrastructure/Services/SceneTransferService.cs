using UnityEngine.SceneManagement;
using Zenject;

namespace Confrontation
{
	public interface ISceneTransferService : IService
	{
		void ToScene(string sceneName);
	}

	public class SceneTransferService : ISceneTransferService
	{
		[Inject] public SceneTransferService() { }

		public void ToScene(string sceneName) => SceneManager.LoadScene(sceneName);
	}
}