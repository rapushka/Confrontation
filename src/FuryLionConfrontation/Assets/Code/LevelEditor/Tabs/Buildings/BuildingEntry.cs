using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Confrontation
{
	public class BuildingEntry : SelectableEntryBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly IPurchase _purchase;
		[Inject] private readonly User _user;
		[Inject] private readonly IInputService _input;

		[SerializeField] private TextMeshProUGUI _buildingNameTextMesh;

		private string TextView => _building.Name;

		private void Start() => _buildingNameTextMesh.text = TextView;

		protected override void OnButtonClick() => _purchase.BuyBuilding(_user.Player, _building, _input.ClickedCell);

		public class Factory : PlaceholderFactory<Building, BuildingEntry>
		{
			public BuildingEntry Create(Building building, Transform parent)
			{
				var buildingButton = base.Create(building);
				buildingButton.transform.SetParent(parent);
				return buildingButton;
			}
		}
	}
}