using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingWindow : WindowBase
	{
		public new class Factory : PlaceholderFactory<Object, BuildingWindow> { }
	}
}