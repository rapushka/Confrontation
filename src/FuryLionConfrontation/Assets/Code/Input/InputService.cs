using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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

		private Ray RayFromCursorPosition => Camera.ScreenPointToRay(CursorPosition);

		private Camera Camera => _camera ??= Camera.main;

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

		private void OnPress(InputAction.CallbackContext context)
			=> RaycastToCursor((r) => Debug.Log($"Press on position {r.transform.position}"));

		private void OnRelease(InputAction.CallbackContext context)
			=> RaycastToCursor((r) => Debug.Log($"Release on position {r.transform.position}"));

		private void OnTap(InputAction.CallbackContext context) => RaycastToCursor((r) => Clicked?.Invoke(r));

		private void RaycastToCursor(Action<ClickReceiver> onHit)
		{
			if (IsPointerOverUIObject() == false
			    && IsHitCollider(out var receiver))
			{
				onHit.Invoke(receiver);
			}
		}

		private bool IsHitCollider(out ClickReceiver receiver)
		{
			receiver = null;
			return Physics.Raycast(RayFromCursorPosition, out var hit)
			       && hit.collider.TryGetComponent(out receiver);
		}

		private static bool IsPointerOverUIObject()
		{
			var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
			{
				position = new Vector2(Input.mousePosition.x, Input.mousePosition.y),
			};
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

			return results.Any();
		}
	}
}