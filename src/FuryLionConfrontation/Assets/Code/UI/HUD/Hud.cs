using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _goldenAmountValueTextMesh;

		public int GoldenAmount
		{
			set => _goldenAmountValueTextMesh.text = value.ToString();
		}
	}
}