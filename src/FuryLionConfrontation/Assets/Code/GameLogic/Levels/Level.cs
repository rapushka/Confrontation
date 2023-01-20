using System;
using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(menuName = "Confrontation/Level", fileName = "Level")]
	public class Level : ScriptableObject
	{
		[field: SerializeField] public Data Value { get; set; }

		[Serializable]
		public class Data
		{
			[field: SerializeField] public Cell.Data[,]   Cells    { get; set; }
			[field: SerializeField] public Player.Data[]  Players  { get; set; }
			[field: SerializeField] public Village.Data[] Villages { get; set; }
			[field: SerializeField] public Coordinates    Sizes    { get; private set; }
		}
	}
}