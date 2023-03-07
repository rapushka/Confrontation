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
		[SerializeField] private float _step = 0.03f;
		[SerializeField] private Slider _loadingBar;

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
			=> await FadeTo(@while: (a) => a < 1, step: _step, atEnd: Enable, source);

		private async void Hide(CancellationTokenSource source)
			=> await FadeTo(@while: (a) => a > 0, step: _step * -1, atEnd: Disable, source);

		public void ShowImmediately() => Enable();

		public void HideImmediately() => Disable();

		private void Update() => _loadingBar.value = _sceneTransfer.LoadingProgress;

		private async Task FadeTo(Func<float, bool> @while, float step, Action atEnd, CancellationTokenSource source)
		{
			while (@while.Invoke(_curtain.alpha))
			{
				_curtain.alpha += step;
				if (await source.Token.WaitForUpdate())
				{
					return;
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