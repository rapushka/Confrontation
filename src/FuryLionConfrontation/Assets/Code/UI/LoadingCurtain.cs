using System;
using System.Collections;
using UnityEngine;

namespace Confrontation
{
	public class LoadingCurtain : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _curtain;
		[SerializeField] private float _step = 0.03f;

		private Coroutine _coroutine;

		private void Awake() => DontDestroyOnLoad(this);

		public void Show()
		{
			EnsureStopCoroutine();

			gameObject.SetActive(true);
			_coroutine = StartCoroutine(FadeTo(@while: (a) => a < 1, step: _step, atEnd: Enable));
		}

		public void Hide()
		{
			EnsureStopCoroutine();

			_coroutine = StartCoroutine(FadeTo(@while: (a) => a > 0, step: _step * -1, atEnd: Disable));
		}

		public void ShowImmediately()
		{
			EnsureStopCoroutine();
			Enable();
		}

		public void HideImmediately()
		{
			EnsureStopCoroutine();
			Disable();
		}

		private IEnumerator FadeTo(Func<float, bool> @while, float step, Action atEnd)
		{
			while (@while.Invoke(_curtain.alpha))
			{
				_curtain.alpha += step;
				yield return null;
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

		private void EnsureStopCoroutine()
		{
			if (_coroutine is not null)
			{
				StopCoroutine(_coroutine);
				_coroutine = null;
			}
		}
	}
}