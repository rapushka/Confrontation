using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class ToolTip : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		[SerializeField] private Vector2 _offset;

		private Vector2 Position { set => transform.position = value; }

		public void Show(string withText, Vector2 on)
		{
			_textMesh.text = withText;
			Position = on + _offset;
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}