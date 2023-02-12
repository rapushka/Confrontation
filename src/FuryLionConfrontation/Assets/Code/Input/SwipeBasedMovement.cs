using System.Collections;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeBasedMovement : MonoBehaviour
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly IRoutinesRunnerService _routinesRunner;
		[Inject] private readonly ITimeService _time;

		[SerializeField] private Transform _root;
		[SerializeField] private Vector2 _speed = new(0.25f, 0.25f);

		private readonly WaitForFixedUpdate _waitForFixedUpdate = new();

		private Vector2 _lastCursorPosition;
		private bool _swiping;

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

				var nextPosition = difference * _speed * _time.FixedDeltaTime;

				_root.Translate(nextPosition.AsTopDown());

				yield return _waitForFixedUpdate;
			}
			// ReSharper disable once IteratorNeverReturns - Coroutine will stop external
		}
	}
}