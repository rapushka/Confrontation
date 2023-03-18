using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class BuildingInfoWindow : GameplayWindowBase
	{
		[Inject] private readonly IInputService _input;
		[Inject] private readonly User _user;
		[Inject] private readonly Purchase _purchase;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private Button _upgradeButton;
		[SerializeField] private TextMeshProUGUI _upgradePriceTextMesh;

		private Building _building;

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public override void Open()
		{
			_building = _input.ClickedCell.Building!;
			UpdateView();

			_upgradeButton.onClick.AddListener(OnButtonClick);

			base.Open();
		}

		public override void Close()
		{
			base.Close();

			_upgradeButton.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			if (_purchase.UpgradeBuilding(_user.Player, _building))
			{
				UpdateView();
			}
			else
			{
				_uiMediator.OpenWindow<NotEnoughGoldWindow>();
			}
		}

		private void UpdateView()
		{
			_titleTextMesh.text = _building.ToString();
			_upgradePriceTextMesh.text = _building.IsOnMaxLevel ? "MAX" : $"Upgrade\n{_building.UpgradePrice} G";
			_upgradeButton.interactable = _building.IsOnMaxLevel == false;
		}

		public new class Factory : PlaceholderFactory<Object, BuildingInfoWindow> { }
	}
}