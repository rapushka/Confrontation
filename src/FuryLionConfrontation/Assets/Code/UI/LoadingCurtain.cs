using System;
using System.Threading;
using System.Threading.Tasks;
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

		public void Show() => _routinesRunner.StartRoutine(ShowRoutine);

		public void Hide() => _routinesRunner.StartRoutine(HideRoutine);

		public void ShowImmediately() => EnableCurtain();

		public void HideImmediately() => DisableCurtain();

		private void Update() => _loadingBar.value = _sceneTransfer.LoadingProgress;

		private async void ShowRoutine(CancellationTokenSource source)
		{
			gameObject.SetActive(true);

			await Fade(from: 0f, to: 1f, source.Token);
			EnableCurtain();
		}

		private async void HideRoutine(CancellationTokenSource source)
		{
			await Fade(from: 1f, to: 0f, source.Token);
			DisableCurtain();

			gameObject.SetActive(false);
		}

		private async Task Fade(float from, float to, CancellationToken token)
		{
			while (Math.Abs(_curtain.alpha - to) > Mathf.Epsilon)
			{
				_passedDuration += _time.DeltaTime;
				_curtain.alpha = Mathf.Lerp(from, to, _passedDuration / _fadeDuration);
				if (await token.WaitForUpdate())
				{
					break;
				}
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