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
		private InputAction _touchPress;
		private InputAction _touchPosition;

		public event Action<ClickReceiver> Clicked;

		private Camera Camera => _camera ??= Camera.main;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			_actions = new InputActions();
			_touchPress = _actions.Gameplay.TouchPress;
			_touchPosition = _actions.Gameplay.TouchPosition;
		}

		private void OnEnable() => _actions.Enable();

		private void OnDisable() => _actions.Disable();

		private void Start() => _touchPress.performed += OnClickPerformed;

		private void OnClickPerformed(InputAction.CallbackContext context)
		{
			if (IsPointerOverUIObject())
			{
				return;
			}

			var touchPosition = _touchPosition.ReadValue<Vector2>();
			var ray = Camera.ScreenPointToRay(touchPosition);
			if (Physics.Raycast(ray, out var hit)
			    && hit.collider.TryGetComponent<ClickReceiver>(out var receiver))
			{
				Clicked?.Invoke(receiver);
			}
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