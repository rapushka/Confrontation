using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class LoadingCurtain : MonoBehaviour
	{
		[Inject] private readonly ISceneTransferService _sceneTransfer;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;
		[Inject] private readonly ITimeService _time;

		[SerializeField] private CanvasGroup _curtain;
		[SerializeField] private float _fadeDuration = 1f;
		[SerializeField] private Slider _loadingBar;

		private float _passedDuration;

		private void Awake() => DontDestroyOnLoad(this);

		public async Task Show() => await _routinesRunner.StartRoutine(ShowRoutine);

		public async Task Hide() => await _routinesRunner.StartRoutine(HideRoutine);

		public void ShowImmediately() => EnableCurtain();

		public void HideImmediately() => DisableCurtain();

		private void Update() => _loadingBar.value = _sceneTransfer.LoadingProgress;

		private async Task ShowRoutine()
		{
			gameObject.SetActive(true);

			await Fade(from: 0f, to: 1f);
			EnableCurtain();
		}

		private async Task HideRoutine()
		{
			await Fade(from: 1f, to: 0f);
			DisableCurtain();

			gameObject.SetActive(false);
		}

		private async Task Fade(float from, float to)
		{
			while (Math.Abs(_curtain.alpha - to) > Mathf.Epsilon)
			{
				_passedDuration += _time.DeltaTime;
				_curtain.alpha = Mathf.Lerp(from, to, _passedDuration / _fadeDuration);
				await UniTask.Yield();
			}

			ResetPassedDuration();
		}

		private void EnableCurtain()
		{
			gameObject.SetActive(true);
			_curtain.alpha = 1;
		}

		private void DisableCurtain()
		{
			_curtain.alpha = 0;
			gameObject.SetActive(false);
		}

		private void ResetPassedDuration() => _passedDuration = 0f;
	}
}