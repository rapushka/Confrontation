using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingButton : ButtonBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly GameplayUiMediator _gameplayUiMediator;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private void Start() => _textMesh.text = _building.Name;

		protected override void OnButtonClick()
		{
			_gameplayUiMediator.Build(_building);
			_uiMediator.CloseCurrentWindow();
		}

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