using System.Collections;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeBasedCameraMovement : MonoBehaviour
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;
		[Inject] private readonly ITimeService _time;

		[SerializeField] private Transform _cameraRoot;
		[SerializeField] private float _cameraSpeed = 0.25f;

		private readonly WaitForFixedUpdate _waitForFixedUpdate = new();

		private Vector2 _lastCursorPosition;
		private bool _swiping;

		private float ScaledSpeed => _time.FixedDeltaTime * _cameraSpeed;

		public void OnEnable()
		{
			_inputService.SwipeStart += OnSwipeStart;
			_inputService.SwipeEnd += OnSwipeEnd;
		}

		public void OnDisable()
		{
			_inputService.SwipeStart -= OnSwipeStart;
			_inputService.SwipeEnd -= OnSwipeEnd;
		}

		private void OnSwipeStart(Vector2 position)
		{
			_lastCursorPosition = position;
			_routinesRunner.StartRoutine(Swipe);
		}

		private void OnSwipeEnd() => _routinesRunner.StopRoutine(Swipe);

		private IEnumerator Swipe()
		{
			while (true)
			{
				var difference = _lastCursorPosition - _inputService.CursorPosition;
				_lastCursorPosition = _inputService.CursorPosition;

				var nextPosition = difference * ScaledSpeed;

				_cameraRoot.Translate(nextPosition.AsTopDown());

				yield return _waitForFixedUpdate;
			}
			// ReSharper disable once IteratorNeverReturns - Coroutine will stop external
		}
	}
}