using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		public override void Show()
		{
			Debug.Log($"What you want build on cell: {User.Player.ClickedCell.Coordinates}");

			base.Show();
		}

		public new class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}