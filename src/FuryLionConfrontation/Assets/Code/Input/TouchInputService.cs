using UnityEngine;
using UnityEngine.InputSystem;

namespace Confrontation
{
	public class TouchInputService : MonoBehaviour
	{
		private readonly Raycaster _raycaster = new();

		private InputActions _actions;
		private InputAction _touchPress;
		private InputAction _touchPosition;

		private void Awake()
		{
			// DontDestroyOnLoad(gameObject);
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
			_raycaster.DetectObject(touchPosition);
		}
	}
}