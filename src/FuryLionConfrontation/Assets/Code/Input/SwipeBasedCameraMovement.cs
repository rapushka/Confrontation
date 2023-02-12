using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeBasedCameraMovement : IInitializable, IDisposable
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;

		public void Initialize()
		{
			_inputService.SwipeStart += OnSwipeStart;
			_inputService.SwipeEnd += OnSwipeEnd;
		}

		public void Dispose()
		{
			_inputService.SwipeStart -= OnSwipeStart;
			_inputService.SwipeEnd -= OnSwipeEnd;
		}

		private void OnSwipeStart(Vector3 position)
		{
			_routinesRunner.StartRoutine(Swipe);
		}

		private void OnSwipeEnd()
		{
			_routinesRunner.StopRoutine(Swipe);
		}

		private IEnumerator Swipe()
		{
			for (var i = 0; i < 3; i++)
			{
				Debug.Log($"routine working {i}");
				yield return new WaitForSeconds(0.2f);
			}
		}
	}
}