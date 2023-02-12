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
		[SerializeField] private float _cameraSpeed = 0.01f;

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

		private void OnSwipeEnd() => _swiping = false;

		private IEnumerator Swipe()
		{
			_swiping = true;
			while (_swiping)
			{
				var different = _lastCursorPosition - _inputService.CursorPosition;
				var nextPosition = _cameraRoot.position - different.AsTopDown();
				nextPosition *= _time.DeltaTime * _cameraSpeed;
				_cameraRoot.position = nextPosition;
				yield return null;
			}
		}
	}
}