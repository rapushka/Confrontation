using Confrontation;
using UnityEngine;

namespace Confrontation.Tests
{
	public static class Create
	{
		public static Field Field(Cell cellPrefab) => new(cellPrefab);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();
	}
}