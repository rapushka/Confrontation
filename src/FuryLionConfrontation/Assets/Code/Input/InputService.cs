using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Confrontation
{
	public class InputService : IInputService, IInitializable, IDisposable
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

		public event Action<Vector2> SwipeStart;

		public event Action SwipeEnd;

		public Cell ClickedCell { get; set; }

		public Vector2 CursorPosition => _cursorPosition.ReadValue<Vector2>();

		public Vector3 CursorWorldPosition => RayFromCursorPosition.GetPoint(5f);

		private Ray RayFromCursorPosition => Camera.ScreenPointToRay(CursorPosition);

		private Camera Camera => _camera == true ? _camera : _camera = Camera.main;

		public void Initialize()
		{
			_actions = new InputActions();
			_cursorPosition = _actions.Gameplay.CursorPosition;
			_actionPress = _actions.Gameplay.Press;
			_actionRelease = _actions.Gameplay.Release;
			_actionTap = _actions.Gameplay.Tap;

			_actions.Enable();

			_actionPress.performed += OnPress;
			_actionRelease.performed += OnRelease;
			_actionTap.performed += OnTap;
		}

		public void Dispose()
		{
			_actions.Disable();

			_actionPress.performed -= OnPress;
			_actionRelease.performed -= OnRelease;
			_actionTap.performed -= OnTap;
		}

		private void OnPress(InputAction.CallbackContext context)
		{
			SwipeStart?.Invoke(CursorPosition);
			RaycastToCursor(onHit: StartDragging);
		}

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
			if (InputTools.IsPointerOverUIObject() == false
			    && RayFromCursorPosition.IsHitReceiver(out var receiver))
			{
				onHit.Invoke(receiver);
			}
		}
	}
}