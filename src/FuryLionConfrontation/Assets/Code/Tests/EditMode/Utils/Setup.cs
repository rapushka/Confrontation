using UnityEngine;

namespace Confrontation.Tests
{
	public static class Setup
	{
		public static Field Field(int height = 1, int width = 1) => Create.Field(Create.CellPrefab(), height, width);

		public static GameObject Cell()
		{
			return Object.Instantiate(Create.CellPrefab()).gameObject;
		}
	}
}