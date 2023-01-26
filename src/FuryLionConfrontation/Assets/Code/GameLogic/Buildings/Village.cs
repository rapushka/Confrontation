using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Village : Building
	{
		[field: SerializeField] public List<Cell> CellsInRegion { get; private set; } = new();

		public void AddToRegion(Cell cell)
		{
			CellsInRegion.Add(cell);
			cell.RelatedRegion = this;
		}

		public class Factory : PlaceholderFactory<Component, int, Village> { }
	}
}