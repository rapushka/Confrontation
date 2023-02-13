using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class TextView : MonoBehaviour
	{
		[SerializeField] private TextMeshPro _textMesh;

		public string Text
		{
			set => _textMesh.text = value;
		}
	}
}