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
		[Inject] private Transform _cameraRoot;

		private Vector2 _initialCursorPosition;
		private bool _swiping;

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

		private void OnSwipeStart(Vector2 position)
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
				var different = _initialCursorPosition - _inputService.CursorPosition;
				var translation = (Vector2)_cameraRoot.position - different;
				translation *= _time.DeltaTime * 0.01f;
				_cameraRoot.position = translation;
				yield return null;
			}
		}
	}
}