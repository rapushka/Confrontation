using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Confrontation
{
	public class EnhancedTouchInputService : MonoBehaviour
	{
		private void Awake() => DontDestroyOnLoad(gameObject);

		private void OnEnable()
		{
			EnhancedTouch.TouchSimulation.Enable();
			EnhancedTouch.EnhancedTouchSupport.Enable();
		}

		private void OnDisable()
		{
			EnhancedTouch.TouchSimulation.Disable();
			EnhancedTouch.EnhancedTouchSupport.Disable();
		}

		private void Start() => EnhancedTouch.Touch.onFingerDown += OnFingerDown;

		private void OnFingerDown(EnhancedTouch.Finger finger)
		{
			Debug.Log("finger.screenPosition = " + finger.screenPosition);
		}
	}
}