using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class ReplaceTextToggle : ToggleBase
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		[SerializeField] private string _textOn;
		[SerializeField] private string _textOff;

		protected override void ToggleOn() => _textMesh.text = _textOn;

		protected override void ToggleOff() => _textMesh.text = _textOff;
	}
}