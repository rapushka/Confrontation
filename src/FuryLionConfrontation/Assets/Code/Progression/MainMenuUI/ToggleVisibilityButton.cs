using UnityEngine;

namespace Confrontation
{
	public class ToggleVisibilityButton : ButtonBase
	{
		[SerializeField] private bool _isVisibleInitial;
		[SerializeField] private GameObject _targetObject;

		private bool _isVisible;

		private void Start()
		{
			_isVisible = _isVisibleInitial;
			UpdateTargetState();
		}

		protected override void OnButtonClick()
		{
			_isVisible = _isVisible == false;
			UpdateTargetState();
		}

		private void UpdateTargetState() => _targetObject.SetActive(_isVisible);
	}
}