using System;
using TMPro;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class BuildingInfoWindow : WindowBase
	{
		[Inject] private readonly IInputService _input;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private EventButton _upgradeButton;

		private Building _building;

		public override WindowBase Accept(IWindowVisitor windowVisitor) => windowVisitor.Visit(this);

		public override void Open()
		{
			_building = _input.ClickedCell.Building!;
			UpdateView();

			_upgradeButton.Click += OnButtonClick;

			base.Open();
		}

		public override void Close()
		{
			base.Close();

			_upgradeButton.Click -= OnButtonClick;
		}

		private void OnButtonClick()
		{
			_building.LevelUp();
			UpdateView();
		}

		private void UpdateView() => _titleTextMesh.text = $"{_building.Name} ─ Lvl {_building.Level.ToString()}";

		public new class Factory : PlaceholderFactory<Object, BuildingInfoWindow> { }
	}
}