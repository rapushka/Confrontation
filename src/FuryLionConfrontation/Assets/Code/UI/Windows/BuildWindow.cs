using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		public void Start() => Debug.Log($"What you want build on cell: {User.Player.ClickedCell.Coordinates}");

		public new class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}