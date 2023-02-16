using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class CameraSwipeMovement : MonoBehaviour
	{
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly ITimeService _time;
		[Inject] private readonly OrderDirectionLineDrawer _orderDrawer;
		[Inject] private readonly FieldBounds _fieldBounds;

		[SerializeField] private Transform _root;
		[SerializeField] private Vector2 _speed = new(0.25f, 0.25f);
		[SerializeField] private float _maxOutOfBoundsDeviation = 1f;
		[SerializeField] private float _smoothRate = 10f;

		private Vector2 _lastCursorPosition;
		private bool _isSwiping;
		private Vector2 _targetPosition;

		private Vector2 NextPosition => SwipeDelta * ScaledSpeed;

		private Vector2 SwipeDelta => _lastCursorPosition - _inputService.CursorPosition;

		private Vector2 ScaledSpeed => _speed * _time.FixedDeltaTime;

		private float ScaledSmoothRate => _smoothRate * _time.FixedDeltaTime;

		private void Awake() => _targetPosition = _root.position;

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
			if (_isSwiping
			    && _orderDrawer.IsGivingOrder == false
			    && InputUtils.IsPointerOverUIObject() == false)
			{
				Move();
				UpdateCursorPosition();
			}
		}

		private void Move()
		{
			_targetPosition += IsInBounds(_targetPosition + NextPosition) ? NextPosition : Vector2.zero;

			_root.position = Vector3.Lerp(_root.position, _targetPosition.AsTopDown(), ScaledSmoothRate);
		}

		private bool IsInBounds(Vector2 position) => _fieldBounds.IsInBounds(position, _maxOutOfBoundsDeviation);

		private void OnSwipeStart(Vector2 position)
		{
			_lastCursorPosition = position;
			_isSwiping = true;
		}

		private void OnSwipeEnd() => _isSwiping = false;

		private void UpdateCursorPosition() => _lastCursorPosition = _inputService.CursorPosition;
	}
}