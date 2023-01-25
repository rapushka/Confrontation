using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		public new class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}