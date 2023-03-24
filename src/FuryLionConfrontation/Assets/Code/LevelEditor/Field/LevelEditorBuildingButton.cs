using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelEditorBuildingButton : ButtonBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly IPurchase _purchase;
		[Inject] private readonly User _user;
		[Inject] private readonly IInputService _input;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private string TextView => _building.Name;

		private void Start() => _textMesh.text = TextView;

		protected override void OnButtonClick() => _purchase.BuyBuilding(_user.Player, _building, _input.ClickedCell);

		public class Factory : PlaceholderFactory<Building, LevelEditorBuildingButton>
		{
			public LevelEditorBuildingButton Create(Building building, Transform parent)
			{
				var buildingButton = base.Create(building);
				buildingButton.transform.SetParent(parent);
				return buildingButton;
			}
		}
	}
}