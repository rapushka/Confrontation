using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public class PageButton : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;

		[field: SerializeField] public Button Button { get; private set; }

		public int PageNumber { set => _textMesh.text = value.ToString(); }
	}
}