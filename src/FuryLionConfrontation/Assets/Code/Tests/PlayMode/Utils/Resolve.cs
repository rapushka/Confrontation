using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Resolve
	{
		public static List<Building> Buildings(DiContainer container) 
			// ReSharper disable once RedundantEnumerableCastCall - this remove nulls
			=> container.Resolve<IField>().Buildings.OfType<Building>().ToList();
	}
}