using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingWindow : WindowBase
	{
		public class Factory : PlaceholderFactory<Object, BuildingWindow> { }
	}
}