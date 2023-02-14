using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Hud : MonoBehaviour
	{
		[Inject] private readonly User _user;

		[SerializeField] private TextMeshProUGUI _goldenAmountValueTextMesh;

		public void UpdateValues()
		{
			_goldenAmountValueTextMesh.text = _user.Player.Stats.GoldCount.ToString();
		}
	}
}