using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingButton : ButtonBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly IPurchase _purchase;
		[Inject] private readonly User _user;
		[Inject] private readonly IInputService _input;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly IBalanceTable _balanceTable;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private string TextView => $"{_building.Name}\n{_balanceTable.BuildPriceFor(_building)} G";

		private void Start() => _textMesh.text = TextView;

		protected override void OnButtonClick()
		{
			if (PurchaseBuilding())
			{
				_uiMediator.CloseCurrentWindow();
			}
			else
			{
				_uiMediator.OpenWindow<NotEnoughGoldWindow>();
			}
		}

		private bool PurchaseBuilding() => _purchase.BuyBuilding(_user.Player, _building, _input.ClickedCell);

		public class Factory : PlaceholderFactory<Building, BuildingButton>
		{
			public BuildingButton Create(Building building, Transform parent)
			{
				var buildingButton = base.Create(building);
				buildingButton.transform.SetParent(parent);
				return buildingButton;
			}
		}
	}
}