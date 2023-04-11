using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class ToolTip : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;

		public void Show(string withText)
		{
			_textMesh.text = withText;
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}