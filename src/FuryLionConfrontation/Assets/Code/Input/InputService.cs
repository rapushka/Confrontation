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
		private InputAction _actionPress;
		private InputAction _cursorPosition;
		private InputAction _actionRelease;

		public event Action<ClickReceiver> Clicked;

		private Camera Camera => _camera ??= Camera.main;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			_actions = new InputActions();
			_actionPress = _actions.Gameplay.Press;
			_cursorPosition = _actions.Gameplay.CursorPosition;
			_actionRelease = _actions.Gameplay.Release;
		}

		private void OnEnable() => _actions.Enable();

		private void OnDisable() => _actions.Disable();

		private void Start()
		{
			_actions.Gameplay.Tap.performed += (_) => Debug.Log("You fast clicked");
			_actions.Gameplay.Press.performed += (_) => Debug.Log("You start long press");
			_actions.Gameplay.Release.performed += (_) => Debug.Log("You end long press");
		}

		private void OnClickPerformed(InputAction.CallbackContext context)
		{
			if (IsPointerOverUIObject() == false)
			{
				ClickAtObject();
			}
		}

		private void ClickAtObject()
		{
			var touchPosition = _cursorPosition.ReadValue<Vector2>();
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