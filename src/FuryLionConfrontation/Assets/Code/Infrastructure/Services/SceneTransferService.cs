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

		public async Task ToScene(string sceneName) => await ToSceneRoutine(sceneName);

		private async Task ToSceneRoutine(string sceneName)
		{
			var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

			while (loadSceneAsync.isDone)
			{
				ProgressVisualisation(loadSceneAsync.progress);
				await UniTask.Yield();
			}
		}

		private void ProgressVisualisation(float progress) => LoadingProgress = Mathf.Clamp01(progress / 0.9f);

		public bool IsCurrentScene(string sceneName) => SceneManager.GetActiveScene().name == sceneName;
	}
}