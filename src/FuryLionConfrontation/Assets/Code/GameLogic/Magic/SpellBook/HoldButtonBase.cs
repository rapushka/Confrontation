using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Confrontation
{
	public abstract class HoldButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[Inject] private readonly ITimeService _time;

		private bool _handled;
		private bool _isPressed;
		private float _holdTime;

		protected virtual float HoldTime => Constants.HoldButtonTimeBase;

		private bool IsHoldTimeUp => (_holdTime -= _time.RealFixedDeltaTime) <= 0;

		private void Start() => ResetButtonState();

		public void Update()
		{
			if (_isPressed && IsHoldTimeUp)
			{
				HandleHold();
				_handled = true;
				ResetButtonState();
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_isPressed = true;
			_handled = false;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_isPressed && _handled == false)
			{
				HandleClick();
			}

			ResetButtonState();
		}

		protected abstract void HandleClick();

		protected abstract void HandleHold();

		private void ResetButtonState()
		{
			_isPressed = false;
			_holdTime = HoldTime;
		}
	}
}