using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Hud : MonoBehaviour
	{
		[Inject] private readonly User _user;

		[SerializeField] private TextMeshProUGUI _goldenAmountValueTextMesh;

		public void Start()
		{
			if (_user.Player is not null)
			{
				_user.Player.Stats.ValueChanged += UpdateValues;
			}
		}

		private void OnDestroy()
		{
			if (_user.Player is not null)
			{
				_user.Player.Stats.ValueChanged += UpdateValues;
			}
		}

		private void UpdateValues() => _goldenAmountValueTextMesh.text = _user.Player.Stats.GoldCount.ToString();
	}
}