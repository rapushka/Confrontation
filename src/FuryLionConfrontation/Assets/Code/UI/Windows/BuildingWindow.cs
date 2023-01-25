using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingWindow : WindowBase
	{
		public void Start() => Debug.Log($"Info about building on: {User.Player.ClickedCell.Coordinates}");

		public new class Factory : PlaceholderFactory<Object, BuildingWindow> { }
	}
}