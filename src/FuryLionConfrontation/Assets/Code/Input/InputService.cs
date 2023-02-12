using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Confrontation
{
	public class InputService : MonoBehaviour, IInputService
	{
		private Camera _camera;
		private InputActions _actions;
		private InputAction _cursorPosition;
		private InputAction _actionPress;
		private InputAction _actionRelease;
		private InputAction _actionTap;

		public event Action<ClickReceiver> Clicked;

		public event Action<ClickReceiver> DragStart;

		public event Action<ClickReceiver> DragEnd;

		public event Action<Vector3> SwipeStart;

		public event Action SwipeEnd;

		public Vector3 CursorWorldPosition => RayFromCursorPosition.GetPoint(5f);

		public Cell ClickedCell { get; set; }

		private Ray RayFromCursorPosition => Camera.ScreenPointToRay(CursorPosition);

		private Camera Camera => _camera == true ? _camera : _camera = Camera.main;

		private Vector2 CursorPosition => _cursorPosition.ReadValue<Vector2>();

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			_actions = new InputActions();
			_cursorPosition = _actions.Gameplay.CursorPosition;
			_actionPress = _actions.Gameplay.Press;
			_actionRelease = _actions.Gameplay.Release;
			_actionTap = _actions.Gameplay.Tap;
		}

		private void OnEnable() => _actions.Enable();

		private void OnDisable() => _actions.Disable();

		private void Start()
		{
			_actionPress.performed += OnPress;
			_actionRelease.performed += OnRelease;
			_actionTap.performed += OnTap;
		}

		private void OnDestroy()
		{
			_actionPress.performed -= OnPress;
			_actionRelease.performed -= OnRelease;
			_actionTap.performed -= OnTap;
		}

		private void OnPress(InputAction.CallbackContext context) => RaycastToCursor(onHit: StartDragging);

		private void OnRelease(InputAction.CallbackContext context)
		{
			SwipeEnd?.Invoke();
			RaycastToCursor(onHit: DropDragging);
		}

		private void StartDragging(ClickReceiver receiver) => DragStart?.Invoke(receiver);

		private void DropDragging(ClickReceiver receiver) => DragEnd?.Invoke(receiver);

		private void OnTap(InputAction.CallbackContext context) => RaycastToCursor(onHit: ClickedInvoke);

		private void ClickedInvoke(ClickReceiver receiver) => Clicked?.Invoke(receiver);

		private void RaycastToCursor(Action<ClickReceiver> onHit)
		{
			if (InputUtils.IsPointerOverUIObject() == false
			    && RayFromCursorPosition.IsHitReceiver(out var receiver))
			{
				onHit.Invoke(receiver);
			}
		}
	}
}