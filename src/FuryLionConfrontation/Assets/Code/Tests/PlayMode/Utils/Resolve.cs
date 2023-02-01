using System.Collections.Generic;
using Zenject;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Resolve
	{
		public static List<Building> Buildings(DiContainer container) 
			=> container.Resolve<BuildingsGenerator>().Buildings;
	}
}