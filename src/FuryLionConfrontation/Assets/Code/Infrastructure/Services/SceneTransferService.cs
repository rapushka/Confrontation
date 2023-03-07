using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Confrontation
{
	public interface ISceneTransferService : IService
	{
		Task ToScene(string sceneName);

		bool IsCurrentScene(string sceneName);

		float LoadingProgress { get; }
	}

	public class SceneTransferService : ISceneTransferService
	{
		public float LoadingProgress { get; private set; }

		public async Task ToScene(string sceneName)
		{
			var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

			while (loadSceneAsync.isDone == false)
			{
				VisualiseProgress(loadSceneAsync.progress);
				await UniTask.Yield();
			}
		}

		private void VisualiseProgress(float progress) => LoadingProgress = Mathf.Clamp01(progress);

		public bool IsCurrentScene(string sceneName) => SceneManager.GetActiveScene().name == sceneName;
	}
}