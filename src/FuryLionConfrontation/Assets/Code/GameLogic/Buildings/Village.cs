using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class Village : Building
	{
		[field: SerializeField] public List<Cell> CellsInRegion { get; private set; } = new();

		[Serializable]
		public class Data
		{
			[field: SerializeField] public Coordinates Coordinates { get; private set; }

			[field: SerializeField] public List<Coordinates> Cells { get; private set; } = new();
		}
	}
}