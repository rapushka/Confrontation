using System;
using System.Collections;
using UnityEngine;

namespace Confrontation.UI
{
	public class LoadingCurtain : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _curtain;
		[SerializeField] private float _step = 0.03f;

		private void Awake() => DontDestroyOnLoad(this);

		public void Show() => StartCoroutine(FadeTo(@while: (a) => a < 1, step: _step, atEnd: Enable));

		public void Hide() => StartCoroutine(FadeTo(@while: (a) => a > 0, step: _step * -1, atEnd: Disable));

		public void ShowImmediately() => Enable();

		public void HideImmediately() => Disable();

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
	}
}