using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Hud : MonoBehaviour
	{
		[Inject] private readonly User _user;

		[SerializeField] private TextMeshProUGUI _goldenAmountValueTextMesh;
		[SerializeField] private TextMeshProUGUI _manaPointsValueTextMesh;

		public void Start()
		{
			if (_user.Player is not null)
			{
				_user.Player.Resources.Gold.ValueChanged += UpdateGoldView;
				_user.Player.Resources.Mana.ValueChanged += UpdateManaView;

				UpdateGoldView();
				UpdateManaView();
			}
		}

		private void OnDestroy()
		{
			if (_user.Player is not null)
			{
				_user.Player.Resources.Gold.ValueChanged -= UpdateGoldView;
				_user.Player.Resources.Mana.ValueChanged -= UpdateManaView;
			}
		}

		private void UpdateGoldView() => _goldenAmountValueTextMesh.text = _user.Player.Resources.Gold.Count.ToString();

		private void UpdateManaView() => _manaPointsValueTextMesh.text = _user.Player.Resources.Mana.Count.ToString();
	}
}