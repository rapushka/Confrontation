using System;
using UnityEngine;
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
			var touchPosition = _touchPosition.ReadValue<Vector2>();
			var ray = Camera.ScreenPointToRay(touchPosition);
			if (Physics.Raycast(ray, out var hit)
			    && hit.collider.TryGetComponent<ClickReceiver>(out var receiver))
			{
				Clicked?.Invoke(receiver);
			}
		}
	}
}