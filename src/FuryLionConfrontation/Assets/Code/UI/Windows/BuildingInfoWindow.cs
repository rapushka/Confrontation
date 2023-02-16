using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class BuildingInfoWindow : WindowBase
	{
		[Inject] private readonly IInputService _input;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private Button _upgradeButton;

		private Building _building;

		public override WindowBase Accept(IWindowVisitor windowVisitor) => windowVisitor.Visit(this);

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
			_building.LevelUp();
			UpdateView();
		}

		private void UpdateView()
		{
			_titleTextMesh.text = _building.ToString();
			_upgradeButton.interactable = _building.IsOnMaxLevel == false;
		}

		public new class Factory : PlaceholderFactory<Object, BuildingInfoWindow> { }
	}
}