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

		[SerializeField] private CanvasGroup _curtain;
		[SerializeField] private float _fadeDuration = 1f;
		[SerializeField] private Slider _loadingBar;

		private float Step => _fadeDuration * 0.01f;

		private float ReversedStep => Step * -1;

		private void Awake() => DontDestroyOnLoad(this);

		public void Show()
		{
			gameObject.SetActive(true);
			_routinesRunner.StartRoutine(Show);
		}

		public void Hide()
		{
			gameObject.SetActive(true);
			_routinesRunner.StartRoutine(Hide);
		}

		private async void Show(CancellationTokenSource source)
			=> await FadeTo(@while: (a) => a < 1, @do: Step, atEnd: Enable, cancellationToken: source.Token);

		private async void Hide(CancellationTokenSource source)
			=> await FadeTo(@while: (a) => a > 0, @do: ReversedStep, atEnd: Disable, cancellationToken: source.Token);

		public void ShowImmediately() => Enable();

		public void HideImmediately() => Disable();

		private void Update() => _loadingBar.value = _sceneTransfer.LoadingProgress;

		private async Task FadeTo(Func<float, bool> @while, float @do, Action atEnd, CancellationToken cancellationToken)
		{
			while (@while.Invoke(_curtain.alpha))
			{
				_curtain.alpha += @do;
				if (await cancellationToken.WaitForUpdate())
				{
					break;
				}
			}

			atEnd.Invoke();
		}

		private void Enable()
		{
			gameObject.SetActive(true);
			_curtain.alpha = 1;
		}

		private void Disable()
		{
			_curtain.alpha = 0;
			gameObject.SetActive(false);
		}
	}
}