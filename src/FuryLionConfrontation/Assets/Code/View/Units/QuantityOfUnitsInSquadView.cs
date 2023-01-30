using TMPro;
using UnityEngine;

namespace Confrontation
{
	public class QuantityOfUnitsInSquadView : MonoBehaviour
	{
		[SerializeField] private TextMeshPro _textMesh;

		public void UpdateValue(int quantity) => _textMesh.text = quantity.ToString();
	}
}