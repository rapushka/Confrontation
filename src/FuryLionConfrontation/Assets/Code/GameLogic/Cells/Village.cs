using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class Village : Building
	{
		[SerializeField] private List<Cell> _cellsInRegion = new();

		public void AddCellToRegion(Cell cell)
		{
			_cellsInRegion.Add(cell);
			CellToColor(cell, "Materials/HexagonsAsset/Red");
		}

		private void RemoveCellFromRegion(Cell cell)
		{
			_cellsInRegion.Remove(cell);
			CellToColor(cell, "Materials/HexagonsAsset/Default");
		}

		private static void CellToColor(Cell cell, string path)
		{
			var cellRenderer = cell.GetComponentInChildren<Renderer>();
			cellRenderer.material = Resources.Load<Material>(path);
		}
	}
}