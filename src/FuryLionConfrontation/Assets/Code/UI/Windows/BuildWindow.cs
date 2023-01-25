using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		public class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}