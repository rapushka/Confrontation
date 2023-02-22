using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingButton : ButtonBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly Purchase _purchase;
		[Inject] private readonly IBalanceTable _balanceTable;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private void Start() => _textMesh.text = $"{_building.Name}\n{_balanceTable.PriceFor(_building)} G";

		protected override void OnButtonClick() => _purchase.BuyBuilding(_building);

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