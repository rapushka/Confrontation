using System;
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

		[Serializable]
		public class Data
		{
			[field: SerializeField] public Coordinates Coordinates { get; private set; }

			[field: SerializeField] public List<Coordinates> Cells { get; private set; } = new();
		}
	}
}