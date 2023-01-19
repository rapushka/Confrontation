using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class Village : Building
	{
		[field: SerializeField] public Data Value { get; private set; }

		[Serializable]
		public class Data
		{
			[field: SerializeField] public List<Cell> CellsInRegion { get; private set; } = new();
		}
	}
}