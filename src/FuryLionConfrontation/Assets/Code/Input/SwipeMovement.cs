using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeMovement : MonoBehaviour
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly ITimeService _time;
		[Inject] private readonly OrderDirectionLineDrawer _orderDrawer;

		[SerializeField] private Transform _root;
		[SerializeField] private Vector2 _speed = new(0.25f, 0.25f);

		private Vector2 _lastCursorPosition;
		private bool _isSwiping;

		private Vector3 NextPosition => (SwipeDelta * ScaledSpeed).AsTopDown();

		private Vector2 SwipeDelta => _lastCursorPosition - _inputService.CursorPosition;

		private Vector2 ScaledSpeed => _speed * _time.FixedDeltaTime;

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

		private void FixedUpdate()
		{
			if (_isSwiping == false
			    || _orderDrawer.IsGivingOrder)
			{
				return;
			}

			_root.Translate(NextPosition);
			UpdateCursorPosition();
		}

		private void OnSwipeStart(Vector2 position)
		{
			_lastCursorPosition = position;
			_isSwiping = true;
		}

		private void OnSwipeEnd() => _isSwiping = false;

		private void UpdateCursorPosition() => _lastCursorPosition = _inputService.CursorPosition;
	}
}