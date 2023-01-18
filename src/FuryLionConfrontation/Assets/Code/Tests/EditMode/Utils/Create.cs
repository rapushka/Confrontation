using Confrontation;
using UnityEngine;

namespace Tests.EditMode
{
	public static class Create
	{
		public static Field Field(Cell cellPrefab) => new(cellPrefab);

		public static Cell CellPrefab() => new GameObject().AddComponent<Cell>();
	}
}