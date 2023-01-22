using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class Village : Building
	{
		[field: SerializeField] public List<Cell> CellsInRegion { get; private set; } = new();

		public void AddCellToRegion(Cell cell)
		{
			CellsInRegion.Add(cell);
			cell.ToRedRegion();
		}

		public void RemoveCellFromRegion(Cell cell)
		{
			CellsInRegion.Remove(cell);
			cell.ToNeutralRegion();
		}
	}
}