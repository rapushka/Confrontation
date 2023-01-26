using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingWindow : WindowBase
	{
		public override void Show()
		{
			Debug.Log($"Info about building on: {User.Player.ClickedCell.Coordinates}");
			
			base.Show();
		}

		public new class Factory : PlaceholderFactory<Object, BuildingWindow> { }
	}
}