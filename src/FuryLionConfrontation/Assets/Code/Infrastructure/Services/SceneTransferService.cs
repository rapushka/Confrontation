using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Confrontation
{
	public interface ISceneTransferService : IService
	{
		void ToScene(string sceneName);

		bool IsCurrentScene(string sceneName);

		float LoadingProgress { get; }
	}

	public class SceneTransferService : ISceneTransferService
	{
		public float LoadingProgress { get; private set; }

		public void ToScene(string sceneName)
			=> SceneManager.LoadSceneAsync(sceneName)
			               .ToUniTask(Progress.Create<float>(ProgressVisualisation));

		private void ProgressVisualisation(float progress) => LoadingProgress = Mathf.Clamp01(progress / 0.9f);

		public bool IsCurrentScene(string sceneName) => SceneManager.GetActiveScene().name == sceneName;
	}
}