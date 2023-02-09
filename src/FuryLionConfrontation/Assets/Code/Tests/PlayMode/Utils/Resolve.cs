using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Resolve
	{
		public static List<Building> ResolveBuildings(this DiContainer @this) 
			// ReSharper disable once RedundantEnumerableCastCall - it remove nulls
			=> @this.Resolve<IField>().Buildings.OfType<Building>().ToList();
	}
}