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
		[Inject] private readonly ITimeService _time;

		private Vector3 _initialCursorPosition;
		private bool _swiping;

		private Camera Camera => Camera.main;

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
			_initialCursorPosition = position;
			_routinesRunner.StartRoutine(Swipe);
		}

		private void OnSwipeEnd() => _swiping = false;

		private IEnumerator Swipe()
		{
			_swiping = true;
			while (_swiping)
			{
				var direction = _initialCursorPosition - (Vector3)_inputService.CursorPosition;
				var translation = Camera.transform.position - direction;
				translation *= _time.DeltaTime * 0.01f;
				Camera.transform.Translate(translation);
				yield return null;
			}
		}
	}
}