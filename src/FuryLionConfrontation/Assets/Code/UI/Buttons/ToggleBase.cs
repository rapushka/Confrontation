using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public abstract class ToggleBase : MonoBehaviour
	{
		[SerializeField] private Toggle _toggle;

		private void OnEnable() => _toggle.onValueChanged.AddListener(OnValueChanged);

		private void OnValueChanged(bool isOn)
		{
			if (isOn)
			{
				ToggleOn();
			}
			else
			{
				ToggleOff();
			}
		}

		protected abstract void ToggleOff();

		protected abstract void ToggleOn();
	}
}