using System;
using UnityEngine;

namespace Confrontation
{
	public class Level : MonoBehaviour
	{
		[field: SerializeField] public Data Value { get; set; }

		[Serializable]
		public class Data
		{
			[field: SerializeField] public Cell.Data[,]  Cells    { get; set; }
			[field: SerializeField] public Player.Data[] Players  { get; set; }
			[field: SerializeField] public Village[]     Villages { get; set; }
		}
	}
}