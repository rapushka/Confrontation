using UnityEngine.SceneManagement;
using Zenject;

namespace Confrontation
{
	public interface ISceneTransferService : IService
	{
		void ToScene(string sceneName);

		bool IsCurrentScene(string sceneName);
	}

	public class SceneTransferService : ISceneTransferService
	{
		[Inject] public SceneTransferService() { }

		public void ToScene(string sceneName) => SceneManager.LoadScene(sceneName);

		public bool IsCurrentScene(string sceneName) => SceneManager.GetActiveScene().name == sceneName;
	}
}