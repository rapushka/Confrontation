using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingInfoWindow : WindowBase
	{
		[Inject] private readonly IInputService _input;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;

		public override WindowBase Accept(IWindowVisitor windowVisitor) => windowVisitor.Visit(this);

		public override void Open()
		{
			var building = _input.ClickedCell.Building;
			Debug.Assert(building == true);
			_titleTextMesh.text = $"{building.name} ─ Lvl. {building.Level.ToString()}";

			base.Open();
		}

		public new class Factory : PlaceholderFactory<Object, BuildingInfoWindow> { }
	}
}