using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class Player
	{
		[field: SerializeField] public int Id { get; set; }

		public Capital Capital;

		public Cell ClickedCell { get; set; }
	}
}