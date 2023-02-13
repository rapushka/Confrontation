using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SwipeMovement : MonoBehaviour
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly ITimeService _time;
		[Inject] private readonly OrderDirectionLineDrawer _orderDrawer;
		[Inject] private readonly FieldBounds _fieldBounds;

		[SerializeField] private Transform _root;
		[SerializeField] private Vector2 _speed = new(0.25f, 0.25f);
		[SerializeField] private float _maxOutBoundsDistance;

		private Vector2 _lastCursorPosition;
		private bool _isSwiping;

		private Vector3 NextPosition => NextVector2.AsTopDown();

		private Vector2 NextVector2 => SwipeDelta * ScaledSpeed;

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

			Move();
			UpdateCursorPosition();
		}

		private void Move()
		{
			var nextTargetPosition = _root.position + NextPosition;
			if (IsInBounds(nextTargetPosition))
			{
				_root.position = nextTargetPosition;
			}

			_root.position = Vector3.Lerp(transform.position, _root.position, _time.FixedDeltaTime);
		}

		private bool IsInBounds(Vector2 position) => _fieldBounds.Bounds.Distance(position) < _maxOutBoundsDistance;

		private void OnSwipeStart(Vector2 position)
		{
			_lastCursorPosition = position;
			_isSwiping = true;
		}

		private void OnSwipeEnd() => _isSwiping = false;

		private void UpdateCursorPosition() => _lastCursorPosition = _inputService.CursorPosition;
	}
}